using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private static List<Contact> contactsList = new List<Contact>();
    [HttpGet]
    public ActionResult<string> Get()
    {
        return JsonSerializer.Serialize(contactsList);
    }
    [HttpPost]
    public IActionResult Post(string id,string name,string server)
    {
        contactsList.Add(new Contact(id,name,server));
        return StatusCode(200);
    }
}

