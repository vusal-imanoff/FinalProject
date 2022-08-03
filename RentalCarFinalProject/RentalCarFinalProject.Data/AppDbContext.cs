using Microsoft.EntityFrameworkCore;
using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data
{
    public class AppDbContext:DbContext
    {
        //dotnet ef --startup-project ..\RentalCarFinalProject.Api migrations add InitialCreate
        //dotnet ef --startup-project ..\RentalCarFinalProject.Api database update 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Model> Models { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
