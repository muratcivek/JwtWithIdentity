namespace JwtWithIdentity_API.DTOs.UserDto
{
    public class TokenResponseDto
    {
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
