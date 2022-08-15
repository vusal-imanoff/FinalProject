using RentalCarFinalProject.Service.DTOs.TransmissionDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface ITransmissionService
    {
        Task PostAsync(TransmissionPostDTO transmissionPostDTO);
        Task<List<TransmissionListDTO>> GetAllAsync();
        Task<TransmissionGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, TransmissionPutDTO transmissionPutDTO);
        Task DeleteAsync(int? id);
    }
}
