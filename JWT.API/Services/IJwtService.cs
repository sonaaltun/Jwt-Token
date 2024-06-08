using Microsoft.AspNetCore.Identity;

namespace JWT.API.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user);
    }
}
