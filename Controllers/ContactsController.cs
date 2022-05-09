using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    //private readonly ContactsIService? contactsIService;
    //private readonly UsersIService? usersIService;
    private readonly DataContext? db;


    public ContactsController()
    {
        //usersIService = uis;
        db = new DataContext();
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        try
        {
            User? user = db.users.Find(getUser());
            return JsonSerializer.Serialize(db.contacts.Where(c => user.contactsId.Contains(c.id)));
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
            db.users.Find(getUser()).contactsId.Add(id);
            db.Add(new ContactModel(id, name, server));
            db.SaveChanges();
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
            return JsonSerializer.Serialize(db.contacts.Find(id));

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
            db.contacts.Update(new ContactModel(id, name, server));
            db.SaveChanges();
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
            db.contacts.Remove(db.contacts.Find(id));
            db.SaveChanges();
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
            return JsonSerializer.Serialize(db.messages.Where(x => (x.fromId == getUser() && x.toId == id) || (x.toId == getUser() && x.fromId == id)));
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
            db.messages.Add(new Message(content,id,getUser()));
            db.SaveChanges();   
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
            return JsonSerializer.Serialize(db.messages.);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPut]
    [Route("{id}/messages/{id2}")]
    public ActionResult<string> editContactMessage(string id, int id2, string content)
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
    private string getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User? user = db.users.Find(name);
        if (user == null) { return null; }
        return user.idName;
    }
}


