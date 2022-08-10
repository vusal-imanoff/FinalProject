using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class Car : BaseEntity
    {
        public string Plate { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public int BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> FuelId { get; set; }
        public Nullable<int> EngineId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> TransmissionId { get; set; }
        public Nullable<int> YearId { get; set; }
        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Category Category { get; set; }
        public Fuel Fuel { get; set; }
        public Engine Engine { get; set; }
        public Color Color { get; set; }
        public Transmission Transmission { get; set; }
        public Year Year { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<CarImages> CarImages { get; set; }
        public List<CarTags> CarTags { get; set; }
    }
}
