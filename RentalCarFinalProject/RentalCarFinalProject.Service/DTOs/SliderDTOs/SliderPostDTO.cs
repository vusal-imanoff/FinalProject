﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.SliderDTOs
{
    public class SliderPostDTO
    {
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public IFormFile File { get; set; }
    }
    public class SliderPostValidator : AbstractValidator<SliderPostDTO>
    {
        public SliderPostValidator()
        {
            RuleFor(b => b.MainTitle).NotEmpty().MaximumLength(255);
            RuleFor(b => b.SubTitle).NotEmpty().MaximumLength(1000);
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
