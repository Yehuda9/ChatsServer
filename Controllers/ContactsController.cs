using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ChatHub chatHub;

    private readonly UsersIService? usersIService;
    private readonly MessagesIService? messagesIService;
    private readonly static string me = "me";

    public ContactsController(UsersIService uis,MessagesIService mis,ChatHub chatHub)
    {
        usersIService = uis;
        messagesIService = mis;
        this.chatHub = chatHub;
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        try
        {
            return JsonSerializer.Serialize(usersIService.getAllContacts(getUser()));
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPost]
    public IActionResult createContact(string id, string name, string server)
    {
        try
        {    
            usersIService.create(id,name, server);
            var u = usersIService.get(id,server);
            usersIService.addContact(getUser(), u.userId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }

    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> getContact(string id)
    {
        try
        {
            return JsonSerializer.Serialize(usersIService.getContact(getUser(), id));

        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> editContact(string id, string name, string server)
    {
        try
        {
            usersIService.update(usersIService.getContact(getUser(),id).userId,name,server);
            return Ok();

        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> deleteContact(string id)
    {
        try
        {
            usersIService.removeContact(getUser(), id);
            return Ok();

        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpGet]
    [Route("{id}/messages")]
    public ActionResult<string> getContactMessages(string id)
    {
        try
        {
            return JsonSerializer.Serialize(messagesIService.getMessages(getUser(),id));
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPost]
    [Route("{id}/messages")]
    public async Task<IActionResult> createContactMessage(string id, string content)
    {
        try
        {
            messagesIService.addMessage(getUser(),id,content);
            var m = new Message();
            m.created = DateTime.Now;
            m.content = content;
            m.fromId = getUser();
            m.toId = id;
            await chatHub.SendMessage(getUser(),id, content);

            //await chatHub.Clients.All.SendAsync("ReceiveMessage", content);
            //chatHub.Clients.Users(getUser()).SendAsync(id,content);
            //chatHub.Clients.User(chatHub.)
            //var u = chatHub.Clients.User(id.Split(',')[0]);
            //await u.ReceiveMessage(m);
            //await u.SendAsync("ReceiveMessage", m);

            //await chatHub.Clients.AllExcept(this.HttpContext.Connection.Id).ReceiveMessage(m);
            //await chatHub.Clients.All.ReceiveMessage(m);

            //await chatHub.Clients.User(getUser()).ReceiveMessage(getUser(),id,content);
            return Ok();

        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpGet]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> getContactMessage(string id, string id2)
    {
        try
        {
            messagesIService.getMessage(getUser(), id, id2);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPut]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> editContactMessage(string id, string id2, string content)
    {
        try
        {
            messagesIService.updateMessage(getUser(), id, id2, content);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    private string? getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User? user = usersIService.get(name,me);
        if (user == null) { return null; }
        return user.userId;
    }
}


