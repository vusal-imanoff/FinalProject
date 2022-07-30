using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Core
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
