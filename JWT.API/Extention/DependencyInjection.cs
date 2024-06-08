using JWT.API.Context;
using JWT.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;
using System.Text;

namespace JWT.API.Extention
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<JwtContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>opt.TokenValidationParameters = new()
            {
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false
            });
            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<JwtContext>().AddDefaultTokenProviders();
            services.AddScoped<IJwtService,JwtService>();
            return services;
        }
    }
}
