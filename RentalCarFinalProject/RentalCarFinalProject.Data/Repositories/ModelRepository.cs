using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
