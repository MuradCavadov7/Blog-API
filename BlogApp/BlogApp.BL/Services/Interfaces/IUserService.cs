using BlogApp.BL.DTOs.UserDto;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> Create(UserCreateDto dto);
        Task<bool> Login(UserLoginDto dto);
        Task<User?> GetUserByUsername(string username);

    }
}
