using AutoMapper;
using BlogApp.BL.DTOs.UserDto;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using System.Security.Cryptography;
using BlogApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.BL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogApp.BL.Exceptions.Common;
using Microsoft.JSInterop.Infrastructure;
using BlogApp.BL.ExternalServices.Interfaces;

namespace BlogApp.BL.Services.Implements
{
    public class AuthService(IUserRepository _repo, IMapper _mapper,IJwtHandler _jwtHandler) : IAuthService
    {
        public async Task RegisterAsync(RegisterDto dto)
        {
            var existUser = await _repo.GetAll().Where(x=>x.Username == dto.Username || x.Email == dto.Email).FirstOrDefaultAsync();
            if (existUser != null)
            {
                if (dto.Username != existUser.Username)
                {
                    throw new ExistException<User>("This username is already available.");
                }
                else if (dto.Email != existUser.Email)
                {
                    throw new ExistException<User>("This email is already available.");
                }

            }

            existUser = _mapper.Map<User>(dto);

            await _repo.AddAsync(existUser);
            await _repo.SaveAsync();

        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _repo.GetByUsernameAsync(username);
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            User? user = null;
            if (dto.UsernameOrEmail.Contains('@'))
            {
                user = await _repo.GetByEmailAsync(dto.UsernameOrEmail);
            }
            else
            {
                user = await _repo.GetByUsernameAsync(dto.UsernameOrEmail);
            }
            if(user == null)
            {
                throw new NotFoundException<User>("User Not Found");
            }
            if(!HashHelper.VerifyHashedPassword(user.PasswordHash,dto.Password))
            {
                throw new NotFoundException<User>("User Not Found");
            }
            
            return _jwtHandler.CreateJwtToken(user,36);
        }
    }
}
