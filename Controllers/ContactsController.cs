using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ContactsIService? contactsIService;
    private readonly UsersIService? usersIService;


    public ContactsController(ContactsIService cis, UsersIService uis)
    {
        contactsIService = cis;
        usersIService = uis;
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        try
        {
            return JsonSerializer.Serialize(contactsIService.getAll(getUser()));
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
            contactsIService.create(getUser(), new Contact(id, name, server));
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
            return JsonSerializer.Serialize(contactsIService.get(getUser(), id));

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
            contactsIService.update(getUser(), new Contact(id, name, server));
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
            contactsIService.delete(getUser(), id);
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
            return JsonSerializer.Serialize((contactsIService.get(getUser(), id).messages));
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPost]
    [Route("{id}/messages")]
    public IActionResult createContactMessage(string id,string content)
    {
        try
        {          
            contactsIService.addMessage(getUser().GetContact(id), content);
            return Ok();

        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpGet]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> getContactMessage(string id, int id2)
    {
        try
        {
            return JsonSerializer.Serialize((contactsIService.get(getUser(), id).messages.Find(x => x.id == id2)));
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPut]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> editContactMessage(string id, int id2,string content)
    {
        try
        {
            contactsIService.editMessage(getUser().GetContact(id), id2, content);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    private User? getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User user = usersIService.get(name);
        if (user == null) { return null; }
        return user;
    }
}


