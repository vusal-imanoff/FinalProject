using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(m => m.Name).IsRequired(true).HasMaxLength(255);
        }
    }
}
