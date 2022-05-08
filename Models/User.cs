using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string idName { get; set; }
    [Required]
    public string password { get; set; }
    public string nickName { get; set; }
    public List<Contact> contacts { get; set; } 
    public User(string id,string password,string name)
    {
        this.idName = id;
        this.password = password;   
        this.nickName = name;
        this.contacts = new List<Contact>();
    }
   public Contact? GetContact(string id)
    {
        return this.contacts.Find(x => x.id == id); 
    }
}

