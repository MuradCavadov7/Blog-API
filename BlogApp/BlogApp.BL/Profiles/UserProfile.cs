using AutoMapper;
using BlogApp.BL.DTOs.UserDto;
using BlogApp.BL.Helper;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(x=>x.PasswordHash, y => y.MapFrom(z=>HashHelper.HashPassword(z.Password)));
        }
    }
}
