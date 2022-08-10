using AutoMapper;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.TagDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Tag tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (tag == null)
            {
                throw new NotFoundException("tag is not found");
            }

            tag.IsDeleted = true;
            tag.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TagListDTO>> GetAllAsync()
        {
            List<TagListDTO> tagListDTOs = _mapper.Map<List<TagListDTO>>(await _unitOfWork.TagRepository.GetAllAsync(t => !t.IsDeleted));
            return tagListDTOs;
        }

        public async Task<TagGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("Id is Required");
            }
            TagGetDTO tagGetDTO = _mapper.Map<TagGetDTO>(await _unitOfWork.TagRepository.GetAsync(t => t.Id == id));
            return tagGetDTO;
        }

        public async Task PostAsync(TagPostDTO tagPostDTO)
        {
            if (await _unitOfWork.TagRepository.IsExistsAsync(t=>t.Name==tagPostDTO.Name))
            {
                throw new AlreadyExistsException($"{tagPostDTO.Name} already exists");
            }

            Tag tag = _mapper.Map<Tag>(tagPostDTO);

            await _unitOfWork.TagRepository.AddAsync(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, TagPutDTO tagPutDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            if (tagPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not matched");
            }

            Tag tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (tag==null)
            {
                throw new NotFoundException("tag is not found");
            }

            if (await _unitOfWork.TagRepository.IsExistsAsync(t=>t.Id!=id && t.Name==tagPutDTO.Name))
            {
                throw new AlreadyExistsException($"{tagPutDTO.Name} is already exists");
            }

            tag.Name = tagPutDTO.Name;
            tag.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Tag tag = await _unitOfWork.TagRepository.GetAsync(t => t.Id == id && t.IsDeleted);
            if (tag == null)
            {
                throw new NotFoundException("tag is not found");
            }

            tag.IsDeleted = false;
            tag.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
