using RentalCarFinalProject.Service.DTOs.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IModelService
    {
        Task PostAsync(ModelPostDTO modelPostDTO);
        Task<List<ModelListDTO>> GetAllAsync();
        Task<ModelGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, ModelPutDTO modelPutDTO);
        Task DeleteAsync(int? id);
    }
}
