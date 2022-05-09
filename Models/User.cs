using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string idName { get; set; }
    [Required]
    public string password { get; set; }
    public string nickName { get; set; }
    public readonly ContactsIService? contactsService;
    public User(string id,string password,string name,ContactsIService cis)
    {
        this.idName = id;
        this.password = password;   
        this.nickName = name;
        contactsService = cis;
    }
}

