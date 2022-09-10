using FluentValidation;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CarDTOs
{
    public class CarPutDTO
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscouuntPrice { get; set; }
        public IFormFile File { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<int> TagIds { get; set; }
        public bool IsFree { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public int FuelId { get; set; }
        public int EngineId { get; set; }
        public int ColorId { get; set; }
        public int TransmissionId { get; set; }
        public int YearId { get; set; }
    }

    public class CarPutValidator : AbstractValidator<CarPutDTO>
    {
        public CarPutValidator()
        {
            RuleFor(b => b.Plate).NotEmpty().MaximumLength(255);
            RuleFor(b => b.Description).NotEmpty().MaximumLength(1000);
            RuleFor(b => b.Files).NotEmpty();
            RuleFor(b => b.TagIds).NotEmpty();
            RuleFor(b => b.BrandId).NotEmpty();
            RuleFor(b => b.ModelId).NotEmpty();
            RuleFor(b => b.CategoryId).NotEmpty();
            RuleFor(b => b.FuelId).NotEmpty();
            RuleFor(b => b.EngineId).NotEmpty();
            RuleFor(b => b.TransmissionId).NotEmpty();
            RuleFor(b => b.YearId).NotEmpty();
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

