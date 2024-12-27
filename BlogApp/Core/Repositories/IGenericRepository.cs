using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetWhere(Func<T, bool> predicate);
        Task<bool> IsExistAsync(int id);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        Task DeleteAsync(int id);
        void Delete(T entity);
    }
}
