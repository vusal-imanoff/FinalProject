using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ColorDTOs
{
    public class ColorPostDTO
    {
        public string Name { get; set; }
    }
    public class ColorPostValidator : AbstractValidator<ColorPostDTO>
    {
        public ColorPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
