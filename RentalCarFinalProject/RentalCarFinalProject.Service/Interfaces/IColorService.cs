using RentalCarFinalProject.Service.DTOs.ColorDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IColorService
    {
        Task PostAsync(ColorPostDTO colorPostDTO); 
        Task<List<ColorListDTO>> GetAllAsync();
        Task<List<ColorListDTO>> GetAllForUsersAsync();
        Task<ColorGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, ColorPutDTO colorPutDTO);
        Task DeleteAsync(int? id);
    }
}
