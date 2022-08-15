using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.FuelDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class FuelService : IFuelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FuelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id is Required");
            }

            Fuel fuel = await _unitOfWork.FuelRepository.GetAsync(f =>f.Id == id);
            if (fuel == null)
            {
                throw new NotFoundException($"{fuel.Name} Not found");
            }

            if (!fuel.IsDeleted)
            {
                fuel.IsDeleted = true;
                fuel.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                fuel.IsDeleted = false;
                fuel.DeletedAt = null;
            }


            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FuelListDTO>> GetAllAsync()
        {
            List<FuelListDTO> fuelListDTOs = _mapper.Map<List<FuelListDTO>>(await _unitOfWork.FuelRepository.GetAllAsync(f => !f.IsDeleted));

            return fuelListDTOs;
        }

        public async Task<FuelGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id Is Required");
            }

            FuelGetDTO fuelGetDTO = _mapper.Map<FuelGetDTO>(await _unitOfWork.FuelRepository.GetAsync(f => f.Id == id));

            return fuelGetDTO;
        }

        public async Task PostAsync(FuelPostDTO fuelPostDTO)
        {
            if (await _unitOfWork.FuelRepository.IsExistsAsync(f=>f.Name==fuelPostDTO.Name))
            {
                throw new AlreadyExistsException($"{fuelPostDTO.Name} Already Exists");
            }

            Fuel fuel = _mapper.Map<Fuel>(fuelPostDTO);

            await _unitOfWork.FuelRepository.AddAsync(fuel);

            await _unitOfWork.CommitAsync(); 
        }

        public async Task PutAsync(int? id, FuelPutDTO fuelPutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is Required");
            }

            if (fuelPutDTO.Id!=id)
            {
                throw new BadRequestException("Id is Not Matched");
            }

            Fuel fuel = await _unitOfWork.FuelRepository.GetAsync(f => !f.IsDeleted && f.Id == id);
            if (fuel==null)
            {
                throw new NotFoundException($"{fuel.Name} Not found");
            }

            if (await _unitOfWork.FuelRepository.IsExistsAsync(f=>f.Id!=fuelPutDTO.Id && f.Name == fuelPutDTO.Name))
            {
                throw new AlreadyExistsException($"{fuel.Name} fuel Already Exists");
            }

            fuel.Name = fuelPutDTO.Name;
            fuel.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync(); 
        }

    }
}
