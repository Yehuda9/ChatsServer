using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly UsersIService? usersIService;
    private readonly MessagesIService? messagesIService;

    public ContactsController(UsersIService uis)
    {
        usersIService = uis;
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

            usersIService.create(id, server);
            usersIService.addContact(getUser(), id + "," + server);
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
    public IActionResult createContactMessage(string id, string content)
    {
        try
        {
            messagesIService.addMessage(getUser(),id,content);
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
        User user = usersIService.get(name);
        if (user == null) { return null; }
        return user.name + ",";
    }
}


