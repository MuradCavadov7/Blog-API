using BlogApp.BL.DTOs.CategoryDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Validators.Category
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(32)
                .WithMessage("The length should not be more than 32");

            RuleFor(x => x.Icon)
                .NotNull()
                .NotEmpty()
                .WithMessage("Icon Url cannot be empty")
                .Matches("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$")
                .WithMessage("Icon must be link")
                .MaximumLength(128)
                .WithMessage("The lenth should not be more than 128");
        }
    }
}
