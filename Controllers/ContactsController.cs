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


    public ContactsController(ContactsIService cis,UsersIService uis)
    {
        contactsIService = cis;
        usersIService = uis;
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        return JsonSerializer.Serialize(contactsIService.getAll(getUser()));
    }
    [HttpPost]
    public IActionResult createContact(string id, string name, string server)
    {
        contactsIService.create(getUser(),new Contact(id, name, server));
        return Ok();
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> getContact(string id)
    {
        return JsonSerializer.Serialize(contactsIService.get(getUser(), id));
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> editContact(string id, string name, string server)
    {
        contactsIService.update(getUser(), new Contact(id, name, server));
        return Ok();
    }
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> deleteContact(string id)
    {
        contactsIService.delete(getUser(), id);
        return Ok();
    }
    [HttpGet]
    [Route("{id}/messages")]
    public ActionResult<string> getContactMessages(string id)
    {
       return JsonSerializer.Serialize((contactsIService.get(getUser(), id).chats));
    }
    private User getUser()
    {
        string name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User user = usersIService.getUserByName(name);
        if (user == null) { return null; }
        return user;
    }
}


