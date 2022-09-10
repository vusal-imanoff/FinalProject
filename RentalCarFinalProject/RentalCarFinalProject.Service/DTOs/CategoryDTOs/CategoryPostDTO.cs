using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CategoryDTOs
{
    public class CategoryPostDTO
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
    public class CategoryPostValidator : AbstractValidator<CategoryPostDTO>
    {
        public CategoryPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
            RuleFor(b => b).Custom((x, context) =>
            {
                if (x.File != null)
                {
                    if (x.File.CheckFileContextType("image/jpeg"))
                    {
                        context.AddFailure("Please Select Correct Image Type. Example Jpeg or Jpg");
                    }
                    if (x.File.CheckFileSize(2000))
                    {
                        context.AddFailure("Please Select Coorect Image Size. Maximum 2 MB");
                    }
                }
            });
        }
    }
}
