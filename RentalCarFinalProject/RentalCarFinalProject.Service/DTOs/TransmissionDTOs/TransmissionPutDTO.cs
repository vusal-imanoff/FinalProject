using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.TransmissionDTOs
{
    public class TransmissionPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TransmissionPutValidator : AbstractValidator<TransmissionPutDTO>
    {
        public TransmissionPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
