using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly ChatHub chatHub;
    private readonly UsersIService? usersService;
    private readonly MessagesIService? messagesIService;
    private readonly static string me = "me";

    public HomeController(UsersIService usersIService, MessagesIService mis, ChatHub chatHub)
    {
        usersService = usersIService;
        this.chatHub = chatHub;
        messagesIService = mis;
    }

    [HttpPost]
    [Route("invitations")]
    public async Task<IActionResult> addConversation([FromBody] InvitationsPayLoad inv)
    {
        if (inv == null || inv.from == null || inv.to == null || inv.server == null) { return BadRequest(); }   
        usersService.create(inv.from, inv.from, inv.server);
        var from = usersService.get(inv.from, inv.server);
        usersService.addContact(inv.to + "," + me, from.userId);
        return Ok();

        /*if (pl == null) return NotFound("Wrong invitaion.");
        // server means who sent the message. Inner inv. are taken care in other cmd
        if (pl.server == me) return Ok("This is an inside-DB invitation. Ignoring.");

        // check if the destination IS in our db
        User? to = usersService.get(pl.to, me);
        if (to == null) return NotFound("No such user (failed finding 'to').");
        User? from = usersService.get(pl.from, pl.server);
        if (from == null) from = new User(pl.from, pl.server, "");
        string response;
        if (usersService.getContact(from.userId, to.userId) == null)
        {
            usersService.addContact(from.userId, to.userId);
            response = "Conversation established.";
            await chatHub.SendMessage(from.userId, to.userId, "new chat");
        }
        else
        {
            response = "Conversation already exists.";
        }
        return Ok(response);*/
    }
    
    [HttpPost]
    [Route("transfer")]
    public async Task<IActionResult> newMessageEntering([FromBody] TransferPayload msg)
    {
        if (msg == null || msg.from == null || msg.to == null || msg.content == null) { return BadRequest(); }
        User? to = usersService.get(msg.to, me);//get dest, user in my server
        if (to == null) return BadRequest();//if not user in my server, 400
        var chat = usersService.getChatByName(to.userId, msg.from);//check that chat exists
        if (chat == null) return BadRequest();//if not,400
        var fromId = chat.user2Id == to.userId ? chat.user1Id : chat.user2Id;//fromId 
        if (fromId.Split(",")[1] == me) return BadRequest();
        messagesIService.addMessage(fromId, to.userId, msg.content);
        var m = new Message();
        m.created = DateTime.Now;
        m.content = msg.content;
        m.fromId = fromId;
        m.toId = to.userId;
        await chatHub.SendMessage(fromId, to.userId, m.content);
        return Ok();
    }

    /*   [HttpPost]
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
           //var chat = usersService.getChatByName(to.userId, msg.from);
           if (chat == null)
               return Unauthorized("No chat established between " + msg.from + " and " + msg.to + ".");

           _messagesService.addMessage(chat.user1Id, chat.user2Id, msg.content);

           return Ok("Message saved in DB.");
       }*/

   
}
