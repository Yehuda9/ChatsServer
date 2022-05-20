using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly ChatHub chatHub;
        private readonly UsersIService? usersService;
        private readonly IConfiguration _configuration;
        private readonly static string me = "me";

        public UsersController(IConfiguration configuration, UsersIService usersIService, ChatHub chatHub)
        {
            usersService = usersIService;
            _configuration = configuration;
            this.chatHub = chatHub;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] LoginPayLoad userInfo)
        {
            var user = usersService.get(userInfo.name, me);

            if (user != null && usersService.checkPassword(user, userInfo.password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("name", userInfo.name),
                };

                var token = getToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user,
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register([FromBody] RegisterPayLoad userInfo)
        {
            var userExists = usersService.get(userInfo.name, me);
            if (userExists != null) return Unauthorized("User already exists!");
            usersService.create(userInfo.name, userInfo.nickName, me, userInfo.password);
            var loginPayLoad = new LoginPayLoad
            {
                name = userInfo.name,
                password = userInfo.password
            };
            return login(loginPayLoad);
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