using RentalCarFinalProject.Service.DTOs.EngineDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IEngineService
    {
        Task PostAsync(EnginePostDTO enginePostDTO);
        Task<List<EngineListDTO>> GetAllAsync();
        Task<EngineGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, EnginePutDTO enginePutDTO);
        Task DeleteAsync(int? id);
    }
}
