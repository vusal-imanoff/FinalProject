using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.AppUserDTOs
{
    public class UpdateDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
         public string FinCode { get; set; }
        public string SeriaNumber { get; set; }
        public string DriverLicanse { get; set; }
    
    }

    public class UpdateDTOValidator: AbstractValidator<UpdateDTO>
    {
        public UpdateDTOValidator()
        {
            RuleFor(a => a.Username).NotEmpty().MaximumLength(20).MinimumLength(8);
            RuleFor(a => a.Name).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Surname).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Age).NotEmpty();
            RuleFor(a => a.Email).EmailAddress().NotEmpty();
        }
    }
}
