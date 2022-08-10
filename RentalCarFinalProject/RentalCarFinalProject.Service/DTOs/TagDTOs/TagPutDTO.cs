using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.TagDTOs
{
    public class TagPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TagPutValidator : AbstractValidator<TagPutDTO>
    {
        public TagPutValidator()
        {
            RuleFor(t => t.Name).NotEmpty().MaximumLength(255);
        }
    }
}
