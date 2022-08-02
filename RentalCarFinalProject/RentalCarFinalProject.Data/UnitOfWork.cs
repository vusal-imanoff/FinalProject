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
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBrandRepository BrandRepository => brandRepository != null ? brandRepository : new BrandRepository(_context);

        public ICategoryRepository CategoryRepository => categoryRepository!=null ? categoryRepository : new CategoryRepository(_context);

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
