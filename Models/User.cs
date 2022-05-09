using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string nickName { get; set; }
    public string server { get; set; }

    public IEnumerable<string> contacts { get; set; }
    public User() { }
    public User(string name, string server, string password = "")
    {
        this.id = name + "," + server;
        this.name = name;
        this.password = password;
        this.nickName = name;
        this.contacts = new List<string>();
        this.server = server;
    }
}

