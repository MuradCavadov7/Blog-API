using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogDbContext _context) : base(_context)
        {
        }
    }
}
