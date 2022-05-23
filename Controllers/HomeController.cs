using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> addConversation([FromForm] InvitationsPayLoad inv)
    {
        if (inv == null || inv.from == null || inv.to == null || inv.server == null) { return BadRequest(); }   
        usersService.create(inv.from, inv.from, inv.server);
        var from = usersService.get(inv.from, inv.server);
        usersService.addContact(inv.to + "," + me, from.userId);
        var to = usersService.get(inv.to, me);
        await chatHub.SendMessage(from.userId, to.userId, inv.server);
        return StatusCode(201);
    }
    
    [HttpPost]
    [Route("transfer")]
    public async Task<IActionResult> newMessageEntering([FromForm] TransferPayload msg)
    {
        if (msg == null || msg.from == null || msg.to == null || msg.content == null) { return BadRequest(); }
        
        User? to = usersService.get(msg.to,me);//get dest, user in my server
        if (to == null) return BadRequest();//if not user in my server, 400
        
        var chat = messagesIService.getChatByName(to.userId, msg.from);//check that chat exists
        if (chat == null) return BadRequest();//if not,400
        
        var fromId = chat.user2Id == to.userId ? chat.user1Id : chat.user2Id;//fromId 
        //if (fromId.Split(",")[1] == me) return BadRequest();
        
        messagesIService.addMessage(fromId, to.userId, msg.content);
        /*
        var m = new Message();
        m.created = DateTime.Now;
        m.content = msg.content;
        m.fromId = fromId;
        m.toId = to.userId;
        */
        await chatHub.SendMessage(fromId, to.userId, msg.content);
        return StatusCode(201);
    }
}
