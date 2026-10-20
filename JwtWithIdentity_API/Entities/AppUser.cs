using Microsoft.AspNetCore.Identity;

namespace JwtWithIdentity_API.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
