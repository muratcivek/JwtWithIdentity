using JwtProject_UI.DTOs.UserDtos;
using JwtWithIdentity_API.DTOs.UserDto;

namespace JwtProject_UI.Services.Users
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginUserDto loginUserDto);
    }
    
}
