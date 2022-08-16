using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.CarId).IsRequired(true);
            builder.Property(o => o.AppUserId).IsRequired(true);
            builder.Property(o => o.Price).IsRequired(true).HasColumnType("money");
        }
    }
}
