using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
