using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.TagDTOs
{
    public class TagPostDTO
    {
        public string Name { get; set; }
    }

    public class TagPostValidator : AbstractValidator<TagPostDTO>
    {
        public TagPostValidator()
        {
            RuleFor(t => t.Name).NotEmpty().MaximumLength(255);
        }
    }
}
