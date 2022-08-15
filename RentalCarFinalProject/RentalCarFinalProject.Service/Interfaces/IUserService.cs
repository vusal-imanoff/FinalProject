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
        Task ActiveAsync(string id);
        //Task ResetPasswordAsync(string id, ResetPasswordDTO resetPasswordDTO);
        Task<List<UserListDTO>> GetAll();
    }
}
