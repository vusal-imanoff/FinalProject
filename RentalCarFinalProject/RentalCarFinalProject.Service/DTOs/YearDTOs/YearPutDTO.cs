using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.YearDTOs
{
    public class YearPutDTO
    {
        public int Id { get; set; }
        public int ProductionYear { get; set; }
    }
    public class YearPutValidator : AbstractValidator<YearPutDTO>
    {
        public YearPutValidator()
        {
            RuleFor(b => b.ProductionYear).NotEmpty();
        }
    }
}
