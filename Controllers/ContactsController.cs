using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private static List<Contact> contacts = new List<Contact>();

}

