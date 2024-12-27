using BlogApp.BL.DTOs.CategoryDto;
using BlogApp.BL.DTOs.UserDto;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var data = await _service.GetUserByUsername(username);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateDto dto)
        {
            await _service.Create(dto);
            return Created();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            await _service.Login(dto);
            return Ok();
        }
    }
}
