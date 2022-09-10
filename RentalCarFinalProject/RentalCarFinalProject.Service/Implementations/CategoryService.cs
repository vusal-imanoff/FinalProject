    using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.CategoryDTOs;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
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

            Category category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

            if (category==null)
            {
                throw new NotFoundException($"{category.Name} Not Found");
            }
            if (!category.IsDeleted)
            {
                category.IsDeleted = true;
                category.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                category.IsDeleted = false;
                category.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
            
        }


        public async Task<List<CategoryListDTO>> GetAllAsync()
        {
            List<CategoryListDTO> categoryListDTOs = _mapper.Map<List<CategoryListDTO>>(await _unitOfWork.CategoryRepository.GetAllAsync());
            return categoryListDTOs;
        }

        public async Task<List<CategoryListDTO>> GetAllForUsersAsync()
        {
            List<CategoryListDTO> categoryListDTOs = _mapper.Map<List<CategoryListDTO>>(await _unitOfWork.CategoryRepository.GetAllForAdminAsync(c => !c.IsDeleted));
            return categoryListDTOs;
        }

        public async Task<CategoryGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadImageFormatException("Id is Required");
            }

            CategoryGetDTO categoryGetDTO = _mapper.Map<CategoryGetDTO>(await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id));

            return categoryGetDTO;
        }

        public async Task PostAsync(CategoryPostDTO categoryPostDTO)
        {
            if (await _unitOfWork.CategoryRepository.IsExistsAsync(c=>c.Name==categoryPostDTO.Name))
            {
                throw new AlreadyExistsException($"{categoryPostDTO.Name} Category AlreadyExists");
            }

            Category category = _mapper.Map<Category>(categoryPostDTO);

            if (categoryPostDTO.File != null)
            {

                category.Image = await categoryPostDTO.File.CreateFileAsync(_env, "categories");

            }

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, CategoryPutDTO categoryPutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is Required");
            }

            if (categoryPutDTO.Id!=id)
            {
                throw new BadRequestException("Id is not matched");
            }

            Category category = await _unitOfWork.CategoryRepository.GetAsync(c => !c.IsDeleted && c.Id == id);
            if (category==null)
            {
                throw new NotFoundException($"{category.Name} Not Found");
            }
            if (await _unitOfWork.CategoryRepository.IsExistsAsync(c=>c.Id!=categoryPutDTO.Id && c.Name==categoryPutDTO.Name))
            {
                throw new AlreadyExistsException($"{categoryPutDTO.Name} category already Exists"); 
            }


            if (categoryPutDTO.File != null)
            {
                if (category.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "categories", category.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                category.Image = await categoryPutDTO.File.CreateFileAsync(_env, "categories");

            }

            category.Name = categoryPutDTO.Name;
            category.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

    }
}
