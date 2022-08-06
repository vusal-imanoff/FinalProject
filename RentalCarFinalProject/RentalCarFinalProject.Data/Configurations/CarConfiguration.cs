using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public
        class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Plate).IsRequired(true).HasMaxLength(255);
            builder.Property(c => c.Description).IsRequired(true).HasMaxLength(1000);
            builder.Property(c => c.Price).IsRequired(true).HasColumnType("money");
            builder.Property(c => c.DiscountPrice).IsRequired(true).HasColumnType("money");
            builder.Property(c => c.Image).HasMaxLength(1000);
            builder.Property(c => c.BrandId).IsRequired(true);
        }
    }
}
