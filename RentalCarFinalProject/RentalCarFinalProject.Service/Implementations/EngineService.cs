using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.EngineDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class EngineService : IEngineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EngineService(IMapper mapper, IUnitOfWork unitOfWork)
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


            Engine engine = await _unitOfWork.EngineRepository.GetAsync(e => e.Id == id && !e.IsDeleted);
            if (engine == null)
            {
                throw new NotFoundException($"{engine} not found");
            }
            engine.IsDeleted = true;
            engine.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<EngineListDTO>> GetAllAsync()
        {
            List<EngineListDTO> engineListDTOs = _mapper.Map<List<EngineListDTO>>(await _unitOfWork.EngineRepository.GetAllAsync(e => !e.IsDeleted));
            return engineListDTOs;
        }

        public async Task<EngineGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is Required");
            }

            EngineGetDTO engineGetDTO = _mapper.Map<EngineGetDTO>(await _unitOfWork.EngineRepository.GetAsync(e => e.Id == id));
            return engineGetDTO;
        }

        public async Task PostAsync(EnginePostDTO enginePostDTO)
        {
            if (await _unitOfWork.EngineRepository.IsExistsAsync(e=>e.Name==enginePostDTO.Name))
            {
                throw new AlreadyExistsException($"{enginePostDTO.Name} engine already exists");
            }

            Engine engine = _mapper.Map<Engine>(enginePostDTO);

            await _unitOfWork.EngineRepository.AddAsync(engine);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, EnginePutDTO enginePutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            if (enginePutDTO.Id!=id)
            {
                throw new BadRequestException("Id is not matched");
            }

            Engine engine = await _unitOfWork.EngineRepository.GetAsync(e=>e.Id==id && !e.IsDeleted);
            if (engine==null)
            {
                throw new NotFoundException($"{engine} not found");
            }
            if (await _unitOfWork.EngineRepository.IsExistsAsync(e => e.Id != enginePutDTO.Id && e.Name == enginePutDTO.Name))
            {
                throw new AlreadyExistsException($"{enginePutDTO.Name} already exists");
            }
            engine.Name = enginePutDTO.Name;
            engine.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }


            Engine engine = await _unitOfWork.EngineRepository.GetAsync(e => e.Id == id && e.IsDeleted);
            if (engine == null)
            {
                throw new NotFoundException($"{engine} not found");
            }
            engine.IsDeleted = false;
            engine.DeletedAt = null;
            await _unitOfWork.CommitAsync();
        }
    }
}
