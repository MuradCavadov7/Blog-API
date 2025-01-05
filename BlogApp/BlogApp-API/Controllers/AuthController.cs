using BlogApp.BL.DTOs.CategoryDto;
using BlogApp.BL.DTOs.UserDto;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var data = await _service.GetUserByUsername(username);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _service.RegisterAsync(dto);
            return Created();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            return Ok(await _service.LoginAsync(dto));
        }
    }
}
