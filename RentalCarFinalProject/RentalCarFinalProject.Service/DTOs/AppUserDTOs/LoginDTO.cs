using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.AppUserDTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(a => a.Password).NotEmpty().MinimumLength(8);
            RuleFor(a => a.Email).EmailAddress().NotEmpty();
        }
    }
}
