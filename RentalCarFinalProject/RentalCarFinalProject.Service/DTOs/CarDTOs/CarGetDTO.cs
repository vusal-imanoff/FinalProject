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
        public double DiscouuntPrice { get; set; }
        public string Image { get; set; }
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
}
