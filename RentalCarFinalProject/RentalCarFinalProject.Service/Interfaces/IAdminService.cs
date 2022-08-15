using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IAdminService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
