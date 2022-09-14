using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public bool IsDeActive { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(a => a.Username).NotEmpty().MaximumLength(20).MinimumLength(8);
            RuleFor(a => a.Name).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Surname).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Age).NotEmpty(); 
            RuleFor(a => a.Email).EmailAddress().NotEmpty();
        }
    }
}
