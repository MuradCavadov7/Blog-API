using BlogApp.Core.Entities.Common;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class GenericRepository<T>(BlogDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
    {
        protected DbSet<T> Table => _context.Set<T>();
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public  async Task DeleteAsync(int id)
        {
           T? entity = await GetByIdAsync(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public IQueryable<T> GetAll()
            => Table.AsQueryable();


        public async Task<T?> GetByIdAsync(int id)
            => await Table.FindAsync(id);

        public IQueryable<T> GetWhere(Func<T, bool> predicate) 
            =>Table.Where(predicate).AsQueryable();
        

        public async Task<bool> IsExistAsync(int id)
            => await  Table.AnyAsync(x=>x.Id == id);

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
