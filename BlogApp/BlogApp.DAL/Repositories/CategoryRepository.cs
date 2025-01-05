using BlogApp.Core.Entities;
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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        readonly BlogDbContext _context;
        public CategoryRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            await _context.Categories.AnyAsync(c=> c.Name == name);
            return false;
        }
    }
}
