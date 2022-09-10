using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.OrderDTOs
{
    public class OrderPostDTO
    {
        public string AppUserId { get; set; }
        public int CarId { get; set; }
        public int CompanyId { get; set; }
        public bool IsCard { get; set; }
        public string Owner { get; set; }
        public int CartNumber { get; set; }
        public int CartMonth { get; set; }
        public int CartYear { get; set; }
        public int CVV { get; set; }
        public double Price { get; set; }
    }

    public class OrderPostValidator : AbstractValidator<OrderPostDTO>
    {
        public OrderPostValidator()
        {
            RuleFor(o => o.CarId).NotNull();
            RuleFor(o => o.AppUserId).NotNull();
            RuleFor(o => o.Price).NotNull();
        }
    }
}
