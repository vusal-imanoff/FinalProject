using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Core
{
    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
