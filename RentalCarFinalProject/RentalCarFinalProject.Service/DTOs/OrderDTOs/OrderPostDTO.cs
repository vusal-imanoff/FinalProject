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
