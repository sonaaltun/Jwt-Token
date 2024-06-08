using JWT.API.DTOs;
using JWT.API.Services;
using JWT.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AccountController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromServices]UserManager<IdentityUser> _userManager,RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new IdentityUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                EmailConfirmed = true
            };
            var identityCreateResult = await _userManager.CreateAsync(user,registerDTO.Password);
            if (!identityCreateResult.Succeeded)
            {
                return BadRequest(new AccountResult
                {
                    IsSucces = false,
                    Message = identityCreateResult.ToString()
                });
            }
            return Ok(new AccountResult
            {
                IsSucces = true,
                Message = "Kullanıcı oluşturma başarılı"
            });
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromServices]SignInManager<IdentityUser> _signInManager,LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _signInManager.UserManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
            {
                return BadRequest(new AccountResult
                {
                    IsSucces = false,
                    Message = "Kullanıcı bulunamadı"
                });
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);
            if (!signInResult.Succeeded)
            {
                return BadRequest(new AccountResult
                {
                    IsSucces = false,
                    Message = "Kullanıcı adı veya şifre yanlış"
                });
            }
                return Ok(new AccountResult { IsSucces = true,Message="Giriş başarılı",Token= _jwtService.GenerateJwtToken(user)});
        }
    }
}
