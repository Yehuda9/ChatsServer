using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly INotificationService notificationService;
    private readonly ChatHub chatHub;
    private readonly IConfiguration _configuration;
    private readonly UsersIService? usersIService;
    private readonly MessagesIService? messagesIService;
    private readonly string me;

    public ContactsController(UsersIService uis, MessagesIService mis, ChatHub chatHub, INotificationService ns, IConfiguration configuration)
    {
        _configuration = configuration;
        notificationService = ns;
        usersIService = uis;
        messagesIService = mis;
        this.chatHub = chatHub;
        me = _configuration["myLocalIpv4"] + ":" + _configuration["myPort"];
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        try
        {
            return Ok(usersIService.getAllContacts(getUser()));
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> createContact([FromForm] CreateContactPayLoad ccp)
    {
        if (ccp == null || ccp.id == null | ccp.name == null || ccp.server == null)
        {
            return BadRequest();
        }
        try
        {
            if (ccp.server != me)
            {
                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                HttpClient client = new(clientHandler);
                var from = usersIService.get(getUser());
                var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "from", from.fullName },
                { "to", ccp.id },
                {"server", me}});
                try
                {
                    var response = await client.PostAsync("https://" + ccp.server + "/api/invitations", content);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    try
                    {
                        var response = await client.PostAsync("http://" + ccp.server + "/api/invitations", content);
                    }
                    catch
                    {
                        Console.Write(ex.ToString());
                    }
                    //message not sent
                }
            }
            usersIService.create(ccp.id, ccp.name, ccp.server);
            var u = usersIService.get(ccp.id, ccp.server);
            usersIService.addContact(getUser(), u.userId);
            await chatHub.SendMessage(getUser(), u.userId, ccp.server);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }

    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> getContact(string id)
    {
        try
        {
            return Ok(usersIService.getContact(getUser(), id));

        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> editContact(string id, string name, string server)
    {
        try
        {
            usersIService.update(usersIService.getContact(getUser(), id).userId, name, server);
            return StatusCode(204);

        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> deleteContact(string id)
    {
        try
        {
            usersIService.removeContact(getUser(), id);
            return StatusCode(204);

        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    [HttpGet]
    [Route("{id}/messages")]
    public ActionResult<string> getContactMessages(string id)
    {
        try
        {
            var to = usersIService.getContact(getUser(), id);
            return Ok(messagesIService.getMessages(getUser(), to.userId));
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    [HttpPost]
    [Route("{id}/messages")]
    public async Task<IActionResult> createContactMessage([FromForm] CreateContactMessagePayLoad ccm)
    {
        if (ccm == null || ccm.id == null || (ccm.content == null && ccm.formFile == null)) { return BadRequest(); }
        try
        {
            var to = usersIService.getContact(getUser(), ccm.id);
            if (to != null && to.server != me)
            {
                HttpClientHandler clientHandler = new()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };

                HttpClient client = new(clientHandler);

                var from = usersIService.get(getUser());

                var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "from", from.fullName },
                { "to", to.fullName },
                {"content",ccm.content }});
                try
                {
                    var response = await client.PostAsync("https://" + to.server + "/api/transfer", content);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    try
                    {
                        var response = await client.PostAsync("http://" + to.server + "/api/transfer", content);

                    }
                    catch
                    {
                        Console.Write(ex.ToString());
                    }
                    //messege not sent
                }
            }
            FileModel? file = null;
            if (ccm.formFile != null)
            {
                file = new(ccm.formFile);
            }
            messagesIService.addMessage(getUser(), to.userId, ccm.content, file);
            if (to.androidToken != null)
            {
                NotificationModel notification = new NotificationModel() { DeviceId = to.androidToken, IsAndroiodDevice = true, Title = getUser(), Body = ccm.content };
                await notificationService.SendNotification(notification);

            }
            await chatHub.SendMessage(getUser(), to.userId, ccm.content);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return NotFound();
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
            return NotFound();
        }
    }
    [HttpPut]
    [Route("{id}/messages/{id2}")]
    public async Task<ActionResult<string>> editContactMessage(string id, string id2, string content)
    {
        try
        {
            messagesIService.updateMessage(getUser(), id, id2, content);
            await chatHub.SendMessage(getUser(), id, id2);
            return StatusCode(204);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    [HttpDelete]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> deleteContactMessage(string id, string id2, string msgId)
    {
        try
        {
            messagesIService.deleteMessage(id, id2, msgId);
            return StatusCode(204);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    private string? getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User? user = usersIService.get(name, me);
        if (user == null) { return null; }
        return user.userId;
    }
}


