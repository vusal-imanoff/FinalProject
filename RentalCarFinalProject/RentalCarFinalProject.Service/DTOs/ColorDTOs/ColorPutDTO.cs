using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ColorDTOs
{
    public class ColorPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ColorPutValidator : AbstractValidator<ColorPutDTO>
    {
        public ColorPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
