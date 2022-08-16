using RentalCarFinalProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.OrderDTOs
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int CarId { get; set; }
        public double Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
