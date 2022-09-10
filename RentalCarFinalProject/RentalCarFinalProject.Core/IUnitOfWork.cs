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
        ITagRepository TagRepository { get; }
        IBlogRepository BlogRepository { get; }
        ISliderRepository SliderRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IMessageRepository MessageRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
