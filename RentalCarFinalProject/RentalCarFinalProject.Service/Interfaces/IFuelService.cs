using RentalCarFinalProject.Service.DTOs.FuelDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IFuelService
    {
        Task PostAsync(FuelPostDTO fuelPostDTO);
        Task<List<FuelListDTO>> GetAllAsync();
        Task<List<FuelListDTO>> GetAllForUsersAsync();
        Task<FuelGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, FuelPutDTO fuelPutDTO);
        Task DeleteAsync(int? id);
    }
}
