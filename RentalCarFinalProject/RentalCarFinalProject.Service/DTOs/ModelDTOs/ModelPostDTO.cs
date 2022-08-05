using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ModelDTOs
{
    public class ModelPostDTO
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
    public class ModelPostValidator : AbstractValidator<ModelPostDTO>
    {
        public ModelPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
