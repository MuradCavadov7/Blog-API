using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        readonly BlogDbContext _context;
        readonly HttpContext _httpContext;
        public UserRepository(BlogDbContext context,IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<User?> GetByUsernameAsync(string username)
            => await _context.Users.Where(x=>x.Username == username).FirstOrDefaultAsync();
        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
    }
}
