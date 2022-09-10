using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IBrandService
    {
        Task PostAsync(BrandPostDTO brandPostDTO);

        Task<List<BrandListDTO>> GetAllAsync();
        Task<List<BrandListDTO>> GetAllForUsersAsync();

        Task<BrandGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id,BrandPutDTO brandPutDTO);
        Task DeleteAsync(int? id);
    }
}
