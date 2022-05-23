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
    private readonly HomeController? homeController;

    private readonly UsersIService? usersIService;
    private readonly MessagesIService? messagesIService;
    private readonly static string me = "me";

    public ContactsController(UsersIService uis, MessagesIService mis, ChatHub chatHub, HomeController hm)
    {
        homeController = hm;
        usersIService = uis;
        messagesIService = mis;
        this.chatHub = chatHub;
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
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);
            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "from", getUser() },
                { "to", ccp.id },
                {"server",ccp.server }
            });
            var response = await client.PostAsync("https://"+ccp.server+ "/invitations", content);
            return await homeController.addConversation(new InvitationsPayLoad { from = getUser(), to = ccp.id, server = ccp.server });
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
            return Ok(messagesIService.getMessages(getUser(), id));
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
        if (ccm == null || ccm.id == null || ccm.content == null) { return BadRequest(); }
        try
        {
            FileModel file = null;
            if (ccm.formFile != null)
            {
                file = new(ccm.formFile);
            }
            messagesIService.addMessage(getUser(), ccm.id, ccm.content, file);
            /*var m = new Message();
            m.created = DateTime.Now;
            m.content = ccm.content;
            m.fromId = getUser();
            m.toId = ccm.id;*/
            await chatHub.SendMessage(getUser(), ccm.id, ccm.content);
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
    private string? getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User? user = usersIService.get(name, me);
        if (user == null) { return null; }
        return user.userId;
    }
}


