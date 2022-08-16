using RentalCarFinalProject.Service.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IOrderService
    {
        Task PostAsync(OrderPostDTO orderPostDTO);
        Task<List<OrderListDTO>> GetAllAsync(string user);
        Task<OrderGetDTO> GetByIdAsync(int? id, string user);
        Task DeleteAsync(int? id);
        Task PutAsync(int? id, OrderPutDTO orderPutDTO);
        Task AcceptAsync(int? id);
        Task ProcessAsync(int? id);
        Task EndAsync(int? id);
        Task RejectAsync(int? id);

    }
}
