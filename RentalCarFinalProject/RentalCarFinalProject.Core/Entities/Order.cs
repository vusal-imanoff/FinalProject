using RentalCarFinalProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class Order:BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double Price { get; set; }
    }
}
