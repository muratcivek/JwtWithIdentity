using JwtWithIdentity_API.Entities;

namespace JwtWithIdentity_API.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
