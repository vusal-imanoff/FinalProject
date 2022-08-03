using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class YearRepository : Repository<Year>, IYearRepository
    {
        public YearRepository(AppDbContext context) : base(context)
        {
        }
    }
}
