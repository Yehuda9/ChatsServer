using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string id { get; set; }
    [Required]
    public string password { get; set; }
    public string name { get; set; }
    public List<Contact> contacts { get; set; } 
    public User(string id,string password,string name)
    {
        this.id = id;
        this.password = password;   
        this.name = name;
        this.contacts = new List<Contact>();
    }
}

