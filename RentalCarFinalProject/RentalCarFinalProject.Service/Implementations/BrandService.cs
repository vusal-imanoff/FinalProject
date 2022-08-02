using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => !b.IsDeleted && b.Id == id);
            if (brand == null)
            {
                throw new NotFoundException($"{brand.Name} not found");
            }
            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();

        }
        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => b.IsDeleted && b.Id == id);
            if (brand == null)
            {
                throw new NotFoundException($"{brand.Name} not found");
            }
            brand.IsDeleted = false;
            brand.DeletedAt = null;
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<BrandListDTO>> GetAllAsync()
        {
            List<BrandListDTO> brandListDTO = _mapper.Map<List<BrandListDTO>>(await _unitOfWork.BrandRepository.GetAllAsync(b=>!b.IsDeleted));
            return brandListDTO;
        }

        public async Task<BrandGetDTO> GetByIdAsync(int? id)
        {
            BrandGetDTO brandGet = _mapper.Map<BrandGetDTO>(await _unitOfWork.BrandRepository.GetAsync(b => b.Id == id));
         
            return brandGet;
        }

        public async Task PostAsync(BrandPostDTO brandPostDTO)
        {
            if (await _unitOfWork.BrandRepository.IsExistsAsync(b => b.Name == brandPostDTO.Name))
            {
                throw new AlreadyExistsException($"Category {brandPostDTO.Name} Already Exist.");
            }

            if (brandPostDTO.File != null)
            {
                if (brandPostDTO.File.CheckFileContextType("image/jpeg"))
                {
                    throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                }

                if (brandPostDTO.File.CheckFileSize(50))
                {
                    throw new BadRequestException("Please Select Coorect Image Size. Maximum 50 KB");
                }

                brandPostDTO.Image = await brandPostDTO.File.CreateFileAsync(_env, "uploads");

            }

            Brand brand = _mapper.Map<Brand>(brandPostDTO);

            await _unitOfWork.BrandRepository.AddAsync(brand);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync( int? id, BrandPutDTO brandPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            if (brandPutDTO.Id != id)
            {
                throw new BadRequestException("Id is not Matched");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => !b.IsDeleted && b.Id==id);
            if (brand==null)
            {
                throw new NotFoundException($"{brand.Name} not found");
            }

            if (await _unitOfWork.BrandRepository.IsExistsAsync(b => b.Name == brandPutDTO.Name && b.Id != brandPutDTO.Id))
            {
                throw new AlreadyExistsException($"{brandPutDTO.Name} Brand Already Exist.");
            }
            //if (brandPutDTO.Name == brand.Name)
            //{
            //    throw new AlreadyExistsException($"{brandPutDTO.Name} Brand Already Exist.");
            //}

            

            if (brandPutDTO.File != null)
            {
                if (brandPutDTO.File.CheckFileContextType("image/jpeg"))
                {
                    throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                }

                if (brandPutDTO.File.CheckFileSize(50))
                {
                    throw new BadRequestException("Please Select Coorect Image Size. Maximum 50 KB");
                }

                if (brand.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "uploads", brand.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                brandPutDTO.Image = await brandPutDTO.File.CreateFileAsync(_env, "uploads");

            }

            brand.Name = brandPutDTO.Name;
            brand.UpdatedAt = DateTime.UtcNow.AddHours(4);
            brand.Image = brandPutDTO.Image;

            await _unitOfWork.CommitAsync();

        }


    }
}
