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
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersIService? usersService;
        private readonly MessagesIService? _messagesService;
        private readonly IConfiguration _configuration;
        private readonly static string me = "me";

        public UsersController(IConfiguration configuration,
            UsersIService usersIService,
            MessagesIService messagesIService)
        {
            usersService = usersIService;
            _configuration = configuration;
            _messagesService = messagesIService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(string name, string password)
        {
            var user = usersService.get(name, me);

            if (user != null && usersService.checkPassword(user, password))
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
                    user,
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(string name, string nickName, string password, string profileImage)
        {

            var userExists = usersService.get(name, me);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            usersService.create(name, nickName, me, password);
            return login(name, password);
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
        [HttpPost]
        [Route("invitations")]
        
        public IActionResult addConversation(Invitation pl)
        {
            
            if (pl == null) return NotFound("Wrong invitaion.");
            // server means who sent the message. Inner inv. are taken care in other cmd
            if (pl.server == me) return Ok("This is an inside-DB invitation. Ignoring."); 
            
            // check if the destination IS in our db
            User? to = usersService.get(pl.to, me);
            if (to == null) return NotFound("No such user (failed finding 'to').");
            User? from = usersService.get(pl.from, me);
            if (from == null) from = new User(pl.from, pl.server, "");
            string response;
            if (usersService.getContact(from.userId, to.userId) == null)
            {
                usersService.addContact(from.userId, to.userId);
                response = "Conversation established.";
            }
            else
            {
                response = "Conversation already exists.";
            }
            return Ok(response);
        }
        public class Invitation
        {
            public string from { get; set; }
            public string to { get; set; }
            public string server { get; set; }
        };

        [HttpPost]
        [Route("transfer")]
        public IActionResult newMessageEntering(TransMessage msg)
        {
            if (msg == null) return NotFound("Wrong transferred message.");
            // if msg comes from me, ignore - PROBLEM : no server
            if (usersService.get(msg.from, me) != null) return Ok("This in an inside-DB message. Ignoring.");
            // check that destination IS in db
            User? to = usersService.get(msg.to, me);
            if (to == null) return NotFound("No such user in DB (failed finding '" + msg.to + "').");

            // if no chat exists unable sending

            var chat = usersService.getChatByName(to.userId, msg.from);
            if (chat == null) 
                return Unauthorized("No chat established between " + msg.from + " and " + msg.to + ".");

            _messagesService.addMessage(chat.user1Id, chat.user2Id, msg.content);
            
            return Ok("Message saved in DB.");
        }

        public class TransMessage
        {
            public string from { get; set; }
            public string to { get; set; }
            public string content { get; set; }
        }
    }
}