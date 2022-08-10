using RentalCarFinalProject.Service.DTOs.BlogDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IBlogService
    {
        Task PostAsync(BlogPostDTO blogPostDTO);
        Task<List<BlogListDTO>> GetAllAsync();
        Task<BlogGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, BlogPutDTO blogPutDTO);
        Task DeleteAsync(int? id);
        Task RestoreAsync(int? id);
    }
}
