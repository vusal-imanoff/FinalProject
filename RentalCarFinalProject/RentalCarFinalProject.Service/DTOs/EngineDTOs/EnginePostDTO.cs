using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.EngineDTOs
{
    public class EnginePostDTO
    {
        public string Name { get; set; }
    }
    public class EnginePostValidator : AbstractValidator<EnginePostDTO>
    {
        public EnginePostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
