using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.BlogDTOs;
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
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id);
            if (blog == null)
            {
                throw new NotFoundException("blog not found");
            }

            if (!blog.IsDeleted)
            {
                blog.IsDeleted = true;
                blog.DeletedAt = CustomDateTime.currentDate;
            }
            else
            {
                blog.IsDeleted = false;
                blog.DeletedAt = null;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<BlogListDTO>> GetAllAsync()
        {
            List<BlogListDTO> blogListDTOs = _mapper.Map<List<BlogListDTO>>(await _unitOfWork.BlogRepository.GetAllAsync(b => !b.IsDeleted));
            return blogListDTOs;
        }

        public async Task<BlogGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is rqured");
            }
            BlogGetDTO blogGetDTO = _mapper.Map<BlogGetDTO>(await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id));
            return blogGetDTO;
        }

        public async Task PostAsync(BlogPostDTO blogPostDTO)
        {
            if (await _unitOfWork.BlogRepository.IsExistsAsync(b=>b.Name==blogPostDTO.Name))
            {
                throw new AlreadyExistsException($"{blogPostDTO.Name} is Alredy exists");
            }

            Blog blog = _mapper.Map<Blog>(blogPostDTO);
            if (blogPostDTO.File != null)
            {
                blog.Image = await blogPostDTO.File.CreateFileAsync(_env, "blogs");
            }
            await _unitOfWork.BlogRepository.AddAsync(blog);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, BlogPutDTO blogPutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            if (blogPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not matched");
            }

            Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (blog==null)
            {
                throw new NotFoundException("blog not found");
            }

            if (await _unitOfWork.BlogRepository.IsExistsAsync(b=>b.Id!=blogPutDTO.Id && b.Name==blogPutDTO.Name))
            {
                throw new AlreadyExistsException($"{blogPutDTO.Name} is already exists");
            }

            if (blogPutDTO.File != null)
            {
                if (blog.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "blogs", blog.Image);
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
                blog.Image = await blogPutDTO.File.CreateFileAsync(_env, "blogs");
            }

            blog.Name=blogPutDTO.Name;
            blog.Description=blogPutDTO.Description;
            blog.UpdatedAt = CustomDateTime.currentDate;

            await _unitOfWork.CommitAsync();
        }

    }
}
