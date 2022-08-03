using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class EngineRepository : Repository<Engine>, IEngineRepository
    {
        public EngineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
