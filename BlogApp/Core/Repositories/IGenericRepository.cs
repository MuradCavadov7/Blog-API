using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        Task DeleteAsync(int id);
        void Delete(T entity);
    }
}
