using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.ModelDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModelService(IMapper mapper, IUnitOfWork unitOfWork)
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

            Model model = await _unitOfWork.ModelRepository.GetAsync(m => m.Id == id);

            if (model==null)
            {
                throw new NotFoundException($"{model} not found");
            }
            if (!model.IsDeleted)
            {
                model.IsDeleted = true;
                model.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                model.IsDeleted = false;
                model.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ModelListDTO>> GetAllAsync()
        {
            List<ModelListDTO> modelListDTOs = _mapper.Map<List<ModelListDTO>>(await _unitOfWork.ModelRepository.GetAllAsync(m => !m.IsDeleted));
            return modelListDTOs;
        }

        public async Task<ModelGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            ModelGetDTO modelGetDTO = _mapper.Map<ModelGetDTO>(await _unitOfWork.ModelRepository.GetAsync(m => m.Id == id));
            return modelGetDTO;
        }

        public async Task PostAsync(ModelPostDTO modelPostDTO)
        {
            if (await _unitOfWork.ModelRepository.IsExistsAsync(m=>m.Name==modelPostDTO.Name))
            {
                throw new AlreadyExistsException($"{modelPostDTO.Name} Already Exists");
            }

            Model model = _mapper.Map<Model>(modelPostDTO);

            await _unitOfWork.ModelRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            
        }

        public async Task PutAsync(int? id, ModelPutDTO modelPutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is required");
            }

            if (modelPutDTO.Id!=id)
            {
                throw new BadRequestException("Id is not matched");
            }

            Model model = await _unitOfWork.ModelRepository.GetAsync(m => !m.IsDeleted && m.Id == modelPutDTO.Id);
            if (model==null)
            {
                throw new NotFoundException($"{model.Name} not found");
            }
            if (await _unitOfWork.ModelRepository.IsExistsAsync(m=>m.Id!=modelPutDTO.Id && m.Name==modelPutDTO.Name))
            {
                throw new AlreadyExistsException($"{modelPutDTO.Name} already exists");
            }

            model.Name=modelPutDTO.Name;
            model.BrandId = modelPutDTO.BrandId;
            model.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

    }
}
