using ImageGalleryAPI.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImageGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
       
        private readonly IConfiguration _config;
        public LoginController(UserManager<UserModel> userManager,IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user != null) 
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");

        }

        private async Task<UserModel> Authenticate(UserLogin userLogin)
        {

            var currentUser =  await _userManager.FindByNameAsync(userLogin.UserName);

            if (currentUser != null) 
            {
                return currentUser;
            }

            return null;

          
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName ),
                new Claim(ClaimTypes.Email,user.Email)

            };

            var token = new JwtSecurityToken(_config["jwt:Issuer"],
                 _config["jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: credentials
                 );

                return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
