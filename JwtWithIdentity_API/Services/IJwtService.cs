using JwtWithIdentity_API.Entities;

namespace JwtWithIdentity_API.Services
{
    public interface IJwtService
    {
        public string GenerateToken(AppUser user);
    }
}
