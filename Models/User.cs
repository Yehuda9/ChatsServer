using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public string userId { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string nickName { get; set; }
    public string server { get; set; }
    //public List<string> contacts { get; set; }
    public List<UserMessage> userMessages { get; set; }

    /*public Message message { get; set; }
    public User() { }
    public User(string name, string server, string password = "")
    {
        this.userId = name + "," + server;
        this.name = name;
        this.password = password;
        this.nickName = name;
        this.contacts = new List<string>();
        this.server = server;
    }*/
}

