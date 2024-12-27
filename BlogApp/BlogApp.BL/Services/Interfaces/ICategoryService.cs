using BlogApp.BL.DTOs.CategoryDto;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryGetDto>> GetAllAsync();
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, CategoryUpdateDto dto);
    Task<Category> GetCategoryByIdAsync(int id);
}
