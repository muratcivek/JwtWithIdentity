using JwtWithIdentity_API.DTOs.UserDto;
using JwtWithIdentity_API.Entities;
using JwtWithIdentity_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtWithIdentity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> _userManager, IJwtService _jwtService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Kullanıcı başarıyla oluşturuldu." });
            }

            return BadRequest(new { Message = "Kullanıcı oluşturulurken bir hata oluştu." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.userNameOrEmail) ?? await _userManager.FindByEmailAsync(model.userNameOrEmail);

            var result = await _userManager.CheckPasswordAsync(user, model.password);

            if (!result)
                return BadRequest(new { Message = "Kullanıcı adı veya şifre hatalı." });

            var token = await _jwtService.GenerateTokenAsync(user);

            return Ok(new { Token = token });
        }
    }
}
