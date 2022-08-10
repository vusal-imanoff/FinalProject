using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(s => s.MainTitle).IsRequired(true).HasMaxLength(255);
            builder.Property(s => s.SubTitle).IsRequired(true).HasMaxLength(1000);
            builder.Property(s => s.Image).HasMaxLength(1000);
        }
    }
}
