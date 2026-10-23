using JwtWithIdentity_API.DTOs.RoleDtos;
using JwtWithIdentity_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtWithIdentity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(RoleManager<AppRole> _roleManager,
                                 UserManager<AppUser> _userManager) : ControllerBase
    {

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = createRoleDto.Name });

            if (result.Succeeded)
            {
                return Ok("Rol başarıyla oluşturuldu.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByNameAsync(assignRoleDto.UserName);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            var roleExists = await _roleManager.RoleExistsAsync(assignRoleDto.RoleName);
            if (!roleExists)
            {
                return NotFound("Rol bulunamadı.");
            }
            var result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);
            if (result.Succeeded)
            {
                return Ok("Rol başarıyla atandı.");
            }
            return BadRequest(result.Errors);
        }

    }
}
