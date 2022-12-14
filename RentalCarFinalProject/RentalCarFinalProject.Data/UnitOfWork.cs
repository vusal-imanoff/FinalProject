using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.IRepositories;
using RentalCarFinalProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BrandRepository brandRepository; 
        private readonly CategoryRepository categoryRepository;
        private readonly ColorRepository colorRepository;
        private readonly FuelRepository fuelRepository;
        private readonly YearRepository yearRepository;
        private readonly EngineRepository engineRepository;
        private readonly TransmissionRepository transmissionRepository;
        private readonly ModelRepository modelRepository;
        private readonly CarRepository carRepository;
        private readonly TagRepository tagRepository;
        private readonly BlogRepository blogRepository;
        private readonly SliderRepository sliderRepository;
        private readonly OrderRepository orderRepository;
        private readonly CompanyRepository companyRepository;
        private readonly MessageRepository messageRepository;

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBrandRepository BrandRepository => brandRepository != null ? brandRepository : new BrandRepository(_context);

        public ICategoryRepository CategoryRepository => categoryRepository!=null ? categoryRepository : new CategoryRepository(_context);
        public IColorRepository ColorRepository => colorRepository!=null ? colorRepository : new ColorRepository(_context);
        public IFuelRepository FuelRepository => fuelRepository!=null ? fuelRepository : new FuelRepository(_context);
        public IYearRepository YearRepository => yearRepository!=null ? yearRepository : new YearRepository(_context);
        public IEngineRepository EngineRepository => engineRepository!=null ? engineRepository : new EngineRepository(_context);
        public ITransmissionRepository TransmissionRepository => transmissionRepository!=null ? transmissionRepository : new TransmissionRepository(_context);
        public IModelRepository ModelRepository => modelRepository!=null ? modelRepository : new ModelRepository(_context);
        public ICarRepository CarRepository => carRepository!=null ? carRepository : new CarRepository(_context);
        public ITagRepository TagRepository => tagRepository!=null ? tagRepository : new TagRepository(_context);
        public IBlogRepository BlogRepository => blogRepository!=null ? blogRepository : new BlogRepository(_context);
        public ISliderRepository SliderRepository => sliderRepository!=null ? sliderRepository : new SliderRepository(_context);
        public IOrderRepository OrderRepository => orderRepository!=null ? orderRepository : new OrderRepository(_context);
        public ICompanyRepository CompanyRepository => companyRepository!=null ? companyRepository : new CompanyRepository(_context);
        public IMessageRepository MessageRepository => messageRepository!=null ? messageRepository : new MessageRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
