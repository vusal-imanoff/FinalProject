using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.FuelDTOs
{
    public class FuelPostDTO
    {
        public string Name { get; set; }
    }
    public class FuelPostValidator : AbstractValidator<FuelPostDTO>
    {
        public FuelPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
