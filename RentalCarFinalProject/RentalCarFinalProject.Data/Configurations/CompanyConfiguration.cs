using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(c => c.Description).IsRequired(true).HasMaxLength(255);
            builder.Property(c => c.Image).HasMaxLength(255);
            builder.Property(c => c.Address).IsRequired(true).HasMaxLength(255);
            builder.Property(c => c.PhoneNumber).IsRequired(true).HasMaxLength(255);
            builder.Property(c => c.WorkTime).IsRequired(true).HasMaxLength(255);
        }
    }
}
