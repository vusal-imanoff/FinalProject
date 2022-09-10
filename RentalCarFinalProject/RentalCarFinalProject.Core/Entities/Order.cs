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
        public Nullable<int> CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsCard { get; set; }
        public string Owner { get; set; }
        public int CartNumber { get; set; }
        public int CartMonth { get; set; }
        public int CartYear { get; set; }
        public int CVV { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double Price { get; set; }
    }
}
