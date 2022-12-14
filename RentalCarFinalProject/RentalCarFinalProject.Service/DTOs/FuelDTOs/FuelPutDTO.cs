using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.FuelDTOs
{
    public class FuelPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FuelPutValidator : AbstractValidator<FuelPutDTO>
    {
        public FuelPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
