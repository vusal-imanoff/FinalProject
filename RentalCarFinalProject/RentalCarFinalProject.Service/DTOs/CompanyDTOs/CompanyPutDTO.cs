using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CompanyDTOs
{
    public class CompanyPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkTime { get; set; }
    }

    public class CompanyPutValidator : AbstractValidator<CompanyPutDTO>
    {
        public CompanyPutValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
            RuleFor(c => c.Description).NotEmpty().MaximumLength(255);
            RuleFor(c => c.Address).NotEmpty().MaximumLength(255);
            RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(255);
            RuleFor(c => c.WorkTime).NotEmpty().MaximumLength(255);
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
