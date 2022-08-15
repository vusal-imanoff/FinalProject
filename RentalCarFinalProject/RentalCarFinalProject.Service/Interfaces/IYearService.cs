using RentalCarFinalProject.Service.DTOs.YearDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IYearService
    {
        Task PostAsync(YearPostDTO yearPostDTO);
        Task<List<YearListDTO>> GetAllAsync();
        Task<YearGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, YearPutDTO yearPutDTO);
        Task DeleteAsync(int? id);
    }
}
