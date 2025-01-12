using AutoMapper;
using BlogApp.BL.DTOs.UserDto;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Helper;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Mail;

namespace BlogApp.BL.Services.Implements
{
    public class AuthService(IUserRepository _repo, IMapper _mapper, IJwtHandler _jwtHandler, IMemoryCache _cache) : IAuthService
    {
        public async Task RegisterAsync(RegisterDto dto)
        {
            var existUser = await _repo.GetWhere(x=>x.Username == dto.Username || x.Email == dto.Email).FirstOrDefaultAsync();
            if (existUser != null)
            {
                if (dto.Username != existUser.Username)
                {
                    throw new ExistsException<User>("This username is already available.");
                }
                else if (dto.Email != existUser.Email)
                {
                    throw new ExistsException<User>("This email is already available.");
                }

            }

            existUser = _mapper.Map<User>(dto);

            await _repo.AddAsync(existUser);
            await _repo.SaveAsync();
            await SendVerificationCodeAsync(existUser.Email);

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
                throw new NotFoundException<User>("User p Not Found");
            }
            
            return _jwtHandler.CreateJwtToken(user,36);
        }

        public async Task<int> SendVerificationCodeAsync(string email)
        {
            if (_cache.TryGetValue(email, out var _)) throw new BadRequestException("Email already sent");
            if (!await _repo.IsExistAsync(x => x.Email == email)) throw new NotFoundException<User>("Email not found");
            Random random = new Random();
            int code = random.Next(100000, 999999);
            await SendEmailAsync(email, code);
            _cache.Set(email, code, TimeSpan.FromMinutes(5)); 
            return code;

        }
        private async Task SendEmailAsync(string receiver , int code)
        {
            using SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("muradnc-ab108@code.edu.az", "nghb aqrb grpk taas");
            MailAddress from = new MailAddress("muradnc-ab108@code.edu.az", "Blog App");
            MailAddress to = new MailAddress(receiver);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Your Verification CODE";
            message.Body = $"Code is:{code}";
            await smtpClient.SendMailAsync(message);
        }

        public async Task<bool> VerifyEmailAsync(string email, int code)
        {
            if (!_cache.TryGetValue(email, out int data)) throw new NotFoundException("6 digit code was written incorrectly");
            if(code == data)
            {
                var user = await _repo.GetWhere(x=>x.Email ==  email).FirstOrDefaultAsync();
                user!.EmailConfirmed = true;
                user.Role = user.Role | (int)Roles.Publisher;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
