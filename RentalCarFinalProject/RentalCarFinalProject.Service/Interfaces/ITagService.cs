using RentalCarFinalProject.Service.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface ITagService
    {
        Task PostAsync(TagPostDTO tagPostDTO);
        Task<List<TagListDTO>> GetAllAsync();
        Task<TagGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, TagPutDTO tagPutDTO);
        Task DeleteAsync(int? id);
    }
}
