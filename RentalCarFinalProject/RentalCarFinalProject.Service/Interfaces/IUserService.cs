using RentalCarFinalProject.Service.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task UpdateAsync(UserUpdateDTO userUpdateDTO);
        Task ActiveAsync(string id);
        Task<List<UserListDTO>> GetAll();
        Task<UserGetDTO> GetById(string id);
    }
}
