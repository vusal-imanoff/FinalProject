using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class CarTagsConfiguration : IEntityTypeConfiguration<CarTags>
    {
        public void Configure(EntityTypeBuilder<CarTags> builder)
        {
            builder.Property(x => x.CarId).IsRequired(true);
        }
    }
}
