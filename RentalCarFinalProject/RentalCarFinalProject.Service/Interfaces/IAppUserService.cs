using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IAppUserService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
