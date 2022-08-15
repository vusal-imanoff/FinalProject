using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.YearDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{ 
    public class YearService : IYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public YearService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            Year year = await _unitOfWork.YearRepository.GetAsync(y =>y.Id == id);
            if (year==null)
            {
                throw new NotFoundException($"{year.ProductionYear} not found");
            }
            if (!year.IsDeleted)
            {
                year.IsDeleted = true;
                year.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                year.IsDeleted = false;
                year.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<YearListDTO>> GetAllAsync()
        {
            List<YearListDTO> yearListDTO = _mapper.Map<List<YearListDTO>>(await _unitOfWork.YearRepository.GetAllAsync(y => !y.IsDeleted));
            return yearListDTO;
        }

        public async Task<YearGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is Required");
            }
            YearGetDTO yearGetDTO = _mapper.Map<YearGetDTO>(await _unitOfWork.YearRepository.GetAsync(y=>y.Id==id));
            return yearGetDTO;
        }

        public async Task PostAsync(YearPostDTO yearPostDTO)
        {
            if (await _unitOfWork.YearRepository.IsExistsAsync(y=>y.ProductionYear==yearPostDTO.ProductionYear))
            {
                throw new AlreadyExistsException($"{yearPostDTO.ProductionYear} year alrady exists");
            }

            Year year = _mapper.Map<Year>(yearPostDTO);

            await _unitOfWork.YearRepository.AddAsync(year);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, YearPutDTO yearPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id is Required");
            }

            if (yearPutDTO.Id!=id)
            {
                throw new BadRequestException("Id Is not Matched");
            }

            Year year = await _unitOfWork.YearRepository.GetAsync(y => !y.IsDeleted && y.Id == yearPutDTO.Id);
            if (year == null)
            {
                throw new NotFoundException($"{year.ProductionYear} not found");
            }

            if (await _unitOfWork.YearRepository.IsExistsAsync(y=>y.Id!=yearPutDTO.Id && y.ProductionYear==yearPutDTO.ProductionYear))
            {
                throw new AlreadyExistsException($"{yearPutDTO.ProductionYear} year already exists");
            }

            year.ProductionYear = yearPutDTO.ProductionYear;
            year.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

    }
}
