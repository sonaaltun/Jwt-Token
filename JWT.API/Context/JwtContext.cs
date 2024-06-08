using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.API.Context
{
    public class JwtContext : IdentityDbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {
            
        }
    }
}
