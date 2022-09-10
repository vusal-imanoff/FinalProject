using RentalCarFinalProject.Service.DTOs.CompanyDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface ICompanyService
    {
        Task PostAsync(CompanyPostDTO companyPostDTO);
        Task<List<CompanyListDTO>> GetAllAsync();
        Task<List<CompanyListDTO>> GetAllForUsersAsync();
        Task<CompanyGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, CompanyPutDTO companyPutDTO);
        Task DeleteAsync(int? id);
        Task<PaginatedCompanyListDTO<CompanyListDTO>> GetAllPageIndexAsync(int pageIndex);
    }
}
