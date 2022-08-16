using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.AppUserDTOs
{
    public class ResetPasswordDTO
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
