using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class FuelRepository : Repository<Fuel>, IFuelRepository
    {
        public FuelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
