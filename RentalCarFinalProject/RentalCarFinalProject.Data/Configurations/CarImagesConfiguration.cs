using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class CarImagesConfiguration : IEntityTypeConfiguration<CarImages>
    {
        public void Configure(EntityTypeBuilder<CarImages> builder)
        {
            builder.Property(b => b.Image).HasMaxLength(1000);
            builder.Property(b => b.CarID).IsRequired(true);
        }
    }
}
