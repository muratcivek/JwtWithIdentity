using JwtProject_UI.DTOs.UserDtos;
using JwtProject_UI.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace JwtProject_UI.Controllers
{
    public class AuthController(IUserService _userService) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _userService.LoginAsync(loginUserDto);

            if(result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
                return View();
            }
        }
    }
}
