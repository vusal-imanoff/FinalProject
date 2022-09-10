using RentalCarFinalProject.Service.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface ISliderService
    {
        Task PostAsync(SliderPostDTO sliderPostDTO);
        Task<List<SliderListDTO>> GetAllAsync();
        Task<List<SliderListDTO>> GetAllForUsersAsync();
        Task<SliderGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, SliderPutDTO sliderPutDTO);
        Task DeleteAsync(int? id);
    }
}
