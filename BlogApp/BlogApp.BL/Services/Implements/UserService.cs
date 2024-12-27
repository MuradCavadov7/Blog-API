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

namespace BlogApp.BL.Services.Implements
{
    public class UserService(IUserRepository _repo, IMapper _mapper, IPasswordHasher _passwordHasher) : IUserService
    {
        public async Task<string> Create(UserCreateDto dto)
        {
            var existUser = await GetUserByUsername(dto.Username);
            if (existUser != null)
            {
                throw new ArgumentException($"{dto.Username} is already exists.");
            }

            User user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);

            await _repo.AddAsync(user);
            await _repo.SaveAsync();

            return user.Username;

        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _repo.GetByUsernameAsync(username);
        }

        public async Task<bool> Login(UserLoginDto dto)
        {
            var user = await GetUserByUsername(dto.Username);
            if (user == null) return false;

            var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, dto.Password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
