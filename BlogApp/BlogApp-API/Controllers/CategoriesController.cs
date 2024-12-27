using BlogApp.BL.DTOs.CategoryDto;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var cats = await _service.GetAllAsync();
            return Ok(cats);
        }

        [HttpPut]
        public async Task<IActionResult> GetId(int id)
        {

            var data = await _service.GetCategoryByIdAsync(id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return Created();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _service.DeleteAsync(id);
            return NoContent();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {

            await _service.UpdateAsync(id, dto);
            return Ok();


        }
    }
}
