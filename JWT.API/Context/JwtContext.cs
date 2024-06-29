using JWT.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.API.Context
{
    public class JwtContext : IdentityDbContext<AppUser>
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
        }
    }
}
