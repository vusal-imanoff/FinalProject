using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CarDTOs
{
    public class CarListDTO
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public bool IsFree { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
    }
}
