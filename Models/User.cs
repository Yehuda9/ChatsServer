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
    public List<Chat> userMessages { get; set; }
    public User() { 
        this.userMessages = new List<Chat>();
    }
    public User(string name, string server, string password = "")
    {
        this.userId = name + "," + server;
        this.name = name;
        this.password = password;
        this.nickName = name;
        this.server = server;
        this.userMessages = new List<Chat>();
    }
}

