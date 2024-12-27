using BlogApp.BL.DTOs.UserDto;
using BlogApp.Core.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        readonly IUserRepository _repo;
        public UserCreateDtoValidator(IUserRepository repo)
        {
            _repo = repo;
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Name cannot be empty")
               .MaximumLength(32)
               .WithMessage("The length should not be more than 32");

            RuleFor(x => x.Surname)
               .NotNull()
               .NotEmpty()
               .WithMessage("Name cannot be empty")
               .MaximumLength(32)
               .WithMessage("The length should not be more than 32");
            
            RuleFor(x => x.Image)
               .NotNull()
               .NotEmpty()
               .WithMessage("Name cannot be empty")
                .Matches("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$")
                .WithMessage("Image must be link")
               .MaximumLength(255)
               .WithMessage("The length should not be more than 255");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .MaximumLength(64)
               .WithMessage("The length should not be more than 64")
               .EmailAddress();
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .MaximumLength(32)
                 .WithMessage("The length should not be more than 32")
                 .Must(x =>
                 {
                     return _repo.GetByUsernameAsync(x).Result == null;
                 })
                 .WithMessage("Username is Exist");
        }
    }
}
