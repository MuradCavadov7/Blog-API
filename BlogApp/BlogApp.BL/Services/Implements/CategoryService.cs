using AutoMapper;
using BlogApp.BL.DTOs.CategoryDto;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BL.Services.Implements
{
    public class CategoryService(ICategoryRepository _repo, IMapper _mapper) : ICategoryService
    {
        public async Task<int> CreateAsync(CategoryCreateDto dto)
        {
            if (await _repo.IsExistAsync(dto.Name)) throw new ExistException<Category> ($"{dto.Name} is created");
            Category cat = _mapper.Map<Category>(dto);
            await _repo.AddAsync(cat);
            await _repo.SaveAsync();
            return cat.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) throw new NotFoundException<Category>("The category to be deleted was not found.");
            _repo.Delete(entity);
            await _repo.SaveAsync();

        }

        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            var cat = await _repo.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<CategoryGetDto>>(cat);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var cat = await _repo.GetByIdAsync(id);
            if (cat is null) throw new NotFoundException<Category>("This category is not found");
            return _mapper.Map<Category>(cat);
        }

        public async Task UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var cat = await _repo.GetByIdAsync(id);
            if (cat is null) throw new NotFoundException<Category>("No category found to update.");
            _mapper.Map(dto,cat);
            await _repo.SaveAsync();
        }
    }
}
