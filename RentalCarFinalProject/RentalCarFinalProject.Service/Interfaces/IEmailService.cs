using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Interfaces
{
    public interface IEmailService
    {
        void Register(RegisterDTO registerDTO, string link);
        void ForgotPassword(AppUser user, string url, ForgotPasswordDTO forgotPassword);
    }
}
