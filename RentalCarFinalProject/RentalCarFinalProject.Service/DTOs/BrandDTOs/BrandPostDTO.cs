using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.BrandDTOs
{
    public class BrandPostDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile File { get; set; }
    }

    public class BrandPostValidator : AbstractValidator<BrandPostDTO>
    {
        public BrandPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
            RuleFor(b => b.Image).MaximumLength(255);
            RuleFor(b => b).Custom((x, context) =>
            {
                if (x.File != null)
                {
                    if (x.File.CheckFileContextType("image/jpeg"))
                    {
                        context.AddFailure("Please Select Correct Image Type. Example Jpeg or Jpg");
                    }
                    if (x.File.CheckFileSize(200))
                    {
                        context.AddFailure("Please Select Coorect Image Size. Maximum 50 KB");
                    }
                }
            });
        }
    }
}
