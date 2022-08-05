using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ModelDTOs
{
    public class ModelPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
    public class ModelPutValidator : AbstractValidator<ModelPutDTO>
    {
        public ModelPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
