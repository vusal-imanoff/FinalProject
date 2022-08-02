using Microsoft.EntityFrameworkCore;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data
{
    public class AppDbContext:DbContext
    {
        //dotnet ef --startup-project ..\P225NLayerArchitectura.Api migrations add InitialCreate
        //dotnet ef --startup-project ..\P225NLayerArchitectura.Api update database
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
