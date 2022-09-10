using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.TransmissionDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class TransmissionService : ITransmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransmissionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Transmission transmission = await _unitOfWork.TransmissionRepository.GetAsync(t => t.Id == id);
            if (transmission == null)
            {
                throw new NotFoundException($"{transmission} not found");
            }
            if (!transmission.IsDeleted)
            {
                transmission.IsDeleted = true;
                transmission.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                transmission.IsDeleted = false;
                transmission.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TransmissionListDTO>> GetAllAsync()
        {
            List<TransmissionListDTO> transmissionListDTOs = _mapper.Map<List<TransmissionListDTO>>(await _unitOfWork.TransmissionRepository.GetAllAsync());
            return transmissionListDTOs;
        }

        public async Task<List<TransmissionListDTO>> GetAllForUsersAsync()
        {
            List<TransmissionListDTO> transmissionListDTOs = _mapper.Map<List<TransmissionListDTO>>(await _unitOfWork.TransmissionRepository.GetAllForAdminAsync(t => !t.IsDeleted));
            return transmissionListDTOs;
        }

        public async Task<TransmissionGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            TransmissionGetDTO transmissionGetDTO = _mapper.Map<TransmissionGetDTO>(await _unitOfWork.TransmissionRepository.GetAsync(t => t.Id == id));
            return transmissionGetDTO;
        }

        public async Task PostAsync(TransmissionPostDTO transmissionPostDTO)
        {
            if (await _unitOfWork.TransmissionRepository.IsExistsAsync(t=>t.Name==transmissionPostDTO.Name))
            {
                throw new AlreadyExistsException($"{transmissionPostDTO.Name} transmission already exists");
            }

            Transmission transmission = _mapper.Map<Transmission>(transmissionPostDTO);

            await _unitOfWork.TransmissionRepository.AddAsync(transmission);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, TransmissionPutDTO transmissionPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (transmissionPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not matched");
            }

            Transmission transmission = await _unitOfWork.TransmissionRepository.GetAsync(t => !t.IsDeleted && t.Id == id);
            if (transmission==null)
            {
                throw new NotFoundException($"{transmission} not found");
            }
            if (await _unitOfWork.TransmissionRepository.IsExistsAsync(t=>t.Name==transmissionPutDTO.Name && t.Id!=transmissionPutDTO.Id))
            {
                throw new AlreadyExistsException($"{transmissionPutDTO.Name} transmission already exists");
            }

            transmission.Name = transmissionPutDTO.Name;
            transmission.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

    }
}
