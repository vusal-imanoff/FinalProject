using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.YearDTOs
{
    public class YearPostDTO
    {
        public int ProductionYear { get; set; }
    }
    public class YearPostValidator : AbstractValidator<YearPostDTO>
    {
        public YearPostValidator()
        {
            RuleFor(b => b.ProductionYear).NotEmpty();
        }
    }
}
