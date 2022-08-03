using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class TransmissionRepository : Repository<Transmission>, ITransmissionRepository
    {
        public TransmissionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
