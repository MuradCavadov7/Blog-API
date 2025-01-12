using BlogApp.BL.DTOs.UserDto;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task<User?> GetUserByUsername(string username);
        Task<int> SendVerificationCodeAsync(string email);
        Task<bool> VerifyEmailAsync(string email,int code);

    }
}
