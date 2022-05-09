using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string idName { get; set; }
    [Required]
    public string password { get; set; }
    public string nickName { get; set; }
    public List<Contact> contacts { get; set; } 
    public User() { }
    public User(string id,string name, string password)
    {
        this.idName = id;
        this.password = password;   
        this.nickName = name;
        this.contacts = new List<Contact>();
    }
}

