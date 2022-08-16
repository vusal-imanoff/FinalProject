using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task UpdateAsync(UpdateDTO updateDTO);
        Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
