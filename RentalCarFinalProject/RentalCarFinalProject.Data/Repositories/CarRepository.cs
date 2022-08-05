using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }
    }
}
