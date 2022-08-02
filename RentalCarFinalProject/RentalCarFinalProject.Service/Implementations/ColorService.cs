using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.ColorDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Color color = await _unitOfWork.ColorRepository.GetAsync(c=>!c.IsDeleted && c.Id==id);
            if (color==null)
            {
                throw new NotFoundException($"{color.Name} not found");
            }
            color.IsDeleted = true;
            color.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ColorListDTO>> GetAllAsync()
        {
            List<ColorListDTO> colorListDTOs = _mapper.Map<List<ColorListDTO>>(await _unitOfWork.ColorRepository.GetAllAsync(c => !c.IsDeleted));
            return colorListDTOs;
        }

        public async Task<ColorGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id Is Required");
            }
            ColorGetDTO colorGetDTO = _mapper.Map<ColorGetDTO>(await _unitOfWork.ColorRepository.GetAsync(c => c.Id == id));

            return colorGetDTO;
        }

        public async Task PostAsync(ColorPostDTO colorPostDTO)
        {
            if (await _unitOfWork.ColorRepository.IsExistsAsync(c=>c.Name==colorPostDTO.Name))
            {
                throw new AlreadyExistsException($"{colorPostDTO.Name} color Already Exists");
            }

            Color color = _mapper.Map<Color>(colorPostDTO);

            await _unitOfWork.ColorRepository.AddAsync(color);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, ColorPutDTO colorPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }

            if (colorPutDTO.Id!=id)
            {
                throw new BadRequestException("Id Is Required");
            }

            Color color = await _unitOfWork.ColorRepository.GetAsync(c=>c.Id==id && !c.IsDeleted);

            if (color==null)
            {
                throw new NotFoundException($"{color.Name} not found");
            }

            if (await _unitOfWork.ColorRepository.IsExistsAsync(c=>c.Id!=colorPutDTO.Id && c.Name==colorPutDTO.Name))
            {
                throw new AlreadyExistsException($"{colorPutDTO.Name} Color Already Exists ");
            }

            color.Name = colorPutDTO.Name;
            color.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Color color = await _unitOfWork.ColorRepository.GetAsync(c => c.IsDeleted && c.Id == id);
            if (color == null)
            {
                throw new NotFoundException($"{color.Name} not found");
            }
            color.IsDeleted = false;
            color.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
