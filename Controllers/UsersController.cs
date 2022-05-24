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

        [HttpGet]
        [Route("userExists")]
        public ActionResult<bool> userExists(string name)
        {
            return usersService.get(name, me) != null;
        }
        [Authorize]
        [HttpGet]
        [Route("getUser")]
        public ActionResult<string> getUser()
        {
            string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
            if (name == null) { return NotFound(); }
            User? user = usersService.get(name, me);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromForm] LoginPayLoad userInfo)
        {
            if (userInfo == null || userInfo.name == null || userInfo.password == null) { return BadRequest(); }
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
        public async Task<IActionResult> register([FromForm] RegisterPayLoad userInfo)
        {
            if (userInfo == null || userInfo.name == null || userInfo.password == null || userInfo.nickName == null) { return BadRequest(); }
            Img proImg = null;
            if (userInfo.profileImage == null || userInfo.profileImage.Length == 0)
            {
                string path = Directory.GetCurrentDirectory();
                var picPath = Path.Join(path, "wwwroot\\generic_profile_image.png");
                byte[] bytes = System.IO.File.ReadAllBytes(picPath);
                proImg = new Img(bytes);
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await userInfo.profileImage.CopyToAsync(memoryStream);
                    proImg = new Img(memoryStream.ToArray());
                }
            }
            var userExists = usersService.get(userInfo.name, me);
            if (userExists != null) return Unauthorized("User already exists!");
            usersService.create(userInfo.name, userInfo.nickName, me, proImg, userInfo.password);
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