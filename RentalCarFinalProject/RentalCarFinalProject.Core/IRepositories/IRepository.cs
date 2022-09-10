using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Core.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(params string[] includes);
        Task<List<TEntity>> GetAllForAdminAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] icludes);
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression);
        void Remove(TEntity entity);
    }
}
