using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateJwtToken(IdentityUser user)
        {
            Claim[] claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier,user.Id),
                new(JwtRegisteredClaimNames.Sub,user.Id),
                new(JwtRegisteredClaimNames.Email,user.Email),
                new(JwtRegisteredClaimNames.GivenName,user.Email),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var encodedKed =Encoding.UTF8.GetBytes(_jwtOptions.Secret);//Encoding.UTF8.GetBytes("IYILERHERZAMANKA24NIRIYILERHERZAMANKA24NIR");//securitiykey 32 karakter uzunluğunda olmalıdır.
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKed),SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer:_jwtOptions.Issuer,audience:_jwtOptions.Audience,claims:claims,expires: DateTime.Now.Add(_jwtOptions.ExpiredTime), signingCredentials:signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
