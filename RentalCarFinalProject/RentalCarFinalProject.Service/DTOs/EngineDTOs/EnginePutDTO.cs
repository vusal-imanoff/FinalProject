using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.EngineDTOs
{
    public class EnginePutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EnginePutValidator : AbstractValidator<EnginePutDTO>
    {
        public EnginePutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
