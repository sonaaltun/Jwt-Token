using JWT.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace JWT.API.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(AppUser user);
    }
}
