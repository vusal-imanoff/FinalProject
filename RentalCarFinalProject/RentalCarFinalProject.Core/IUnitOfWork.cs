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
        ICategoryRepository CategoryRepository { get; }
        IColorRepository ColorRepository { get; }
        IFuelRepository FuelRepository { get; }
        IYearRepository YearRepository { get; }
        IEngineRepository EngineRepository { get; }
        ITransmissionRepository TransmissionRepository { get; }
        IModelRepository ModelRepository { get; }
        ICarRepository CarRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
