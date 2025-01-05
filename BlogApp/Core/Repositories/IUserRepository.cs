using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetCurrentUser();
        int GetCurrentUserId();
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);

    }
}
