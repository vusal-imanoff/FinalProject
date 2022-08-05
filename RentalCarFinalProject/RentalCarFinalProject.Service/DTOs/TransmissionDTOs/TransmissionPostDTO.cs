using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.TransmissionDTOs
{
    public class TransmissionPostDTO
    {
        public string Name { get; set; }
    }
    public class TransmissionPostValidator : AbstractValidator<TransmissionPostDTO>
    {
        public TransmissionPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
