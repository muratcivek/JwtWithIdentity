using JwtProject_UI.DTOs.UserDtos;
using JwtWithIdentity_API.DTOs.UserDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtProject_UI.Services.Users
{
    public class UserService(HttpClient _client, IHttpContextAccessor _httpContextAccessor) : IUserService
    {
        public async Task<bool> LoginAsync(LoginUserDto loginUserDto)
        {
            var response = await _client.PostAsJsonAsync("users/login", loginUserDto);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenResponse.Token);
                var claims = token.Claims.ToList();

                claims.Add(new System.Security.Claims.Claim("Token", tokenResponse.Token));

                var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, "jwt");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return true;
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı.");
            }
        }
    }
}
