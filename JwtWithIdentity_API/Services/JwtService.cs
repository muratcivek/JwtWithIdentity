using JwtWithIdentity_API.Entities;
using JwtWithIdentity_API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtWithIdentity_API.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Task<string> GenerateTokenAsync(AppUser user)
        {
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("id", user.Id.ToString()),
                new System.Security.Claims.Claim("userName", user.UserName),
                new System.Security.Claims.Claim("email", user.Email)
            };

            //kullanıcıya ait rolleri çekip claime ekleyeceğiz.

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Task.FromResult(token);
        }
    }
}
