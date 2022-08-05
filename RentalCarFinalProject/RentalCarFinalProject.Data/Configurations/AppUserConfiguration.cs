using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(a=>a.SurName).IsRequired(true).HasMaxLength(255);
            builder.Property(a=>a.Age).IsRequired(true);
        }
    }
}
