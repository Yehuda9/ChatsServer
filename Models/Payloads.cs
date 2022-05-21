using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

public class TransferPayload
{
    public string from { get; set; }
    public string to { get; set; }
    public string content { get; set; }
}
public class InvitationsPayLoad
{
    public string from { get; set; }
    public string to { get; set; }
    public string server { get; set; }
};
public class RegisterPayLoad
{
    public string name { get; set; }
    public string nickName { get; set; }
    public string password { get; set; }
    //[ModelBinder(BinderType = typeof(ByteArrayModelBinder))]
    /*[ModelBinder(Name = "profileImage")]
    public byte[] profileImage { get; set; }*/
    public IFormFile? profileImage { get; set; } 
}

public class LoginPayLoad
{
    public string name { get; set; }
    public string password { get; set; }
}

public class CreateContactMessagePayLoad
{
    public string id { get; set; }
    public string content { get; set; }
}

public class CreateContactPayLoad
{
    public string id { get; set; }
    public string name { get; set; }
    public string server { get; set; }
}