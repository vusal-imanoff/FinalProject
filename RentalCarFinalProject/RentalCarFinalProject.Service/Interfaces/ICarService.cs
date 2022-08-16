using RentalCarFinalProject.Service.DTOs.CarDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface ICarService
    {
        Task PostAsync(CarPostDTO carPostDTO);
        Task<List<CarListDTO>> GetAllAsync();
        Task<PagenetedListDTO<CarListDTO>> GetAllPageIndexAsync(int pageIndex);
        Task<CarGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, CarPutDTO carPutDTO);
        Task DeleteAsync(int? id);
    }
}
