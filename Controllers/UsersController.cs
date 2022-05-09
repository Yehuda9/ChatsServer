using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.NET6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly DbContext userContext;
        private readonly UsersIService? usersService;
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration,UsersIService usersIService)
        {
            usersService = usersIService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public  IActionResult login(string name,string password)
        {
            var user = usersService.get(name);
            

            if (user != null &&  usersService.checkPassword(user, password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("name", name),
                };

                var token = getToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
       /* [Authorize]
        [HttpPost]
        [Route("logout")]
        public  IActionResult logout()
        {

        }*/

        [HttpPost]
        [Route("register")]
        public IActionResult register(string name,string password)
        {

            var userExists =  usersService.get(name);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            usersService.create(name, name, password);
            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken getToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}