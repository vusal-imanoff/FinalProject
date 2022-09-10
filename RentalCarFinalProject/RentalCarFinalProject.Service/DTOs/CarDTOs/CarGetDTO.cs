using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CarDTOs
{
    public class CarGetDTO
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string Image { get; set; }
        public bool IsFree { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CategoryName { get; set; }
        public string FuelName { get; set; }
        public string EngineName { get; set; }
        public string ColorName { get; set; }
        public string TransmissionName { get; set; }
        public string CompanyName { get; set; }
        public int Year { get; set; }
        public List<CarImages> CarImages { get; set; }
        public List<CarTags> CarTags { get; set; }
    }
}
