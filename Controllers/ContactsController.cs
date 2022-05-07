using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private static ContactsIService contactsIService;

    public ContactsController(ContactsIService cis)
    {
        contactsIService = cis;
    }

    [HttpGet]
    public ActionResult<string> getAllContacts()
    {
        return JsonSerializer.Serialize(contactsIService.getAll());
    }
    [HttpPost]
    public IActionResult createContact(string id, string name, string server)
    {
        contactsIService.create(new Contact(id, name, server));
        return StatusCode(200);
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> getContact(string id)
    {
        return JsonSerializer.Serialize(contactsIService.get(id));
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> editContact(string id, string name, string server)
    {
        contactsIService.update(new Contact(id, name, server));
        return StatusCode(200);
    }
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> deleteContact(string id)
    {
        contactsIService.delete(id);
        return StatusCode(200);
    }
    [HttpGet]
    [Route("{id}/messages")]
    public ActionResult<string> getContactMessages(string id)
    {
       return JsonSerializer.Serialize((contactsIService.get(id).chats));
    }



}


