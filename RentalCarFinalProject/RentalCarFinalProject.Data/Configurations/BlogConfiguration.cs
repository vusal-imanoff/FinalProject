using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(b => b.Description).IsRequired(true).HasMaxLength(1000);
            builder.Property(b => b.Image).HasMaxLength(255);
        }
    }
}
