using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {

        }
    }
}
