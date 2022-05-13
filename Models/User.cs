using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

public class User
{
    [Key]
    [IgnoreDataMember]
    public string userId { get; set; }
    [JsonProperty(PropertyName = "id")]
    public string fullName { get; set; }
    //[JsonIgnore]
    public string password { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string nickName { get; set; }
    public string server { get; set; }
    public string last { get; set; }
    public DateTime lastDate { get; set; }

    public List<Chat> userMessages { get; set; }
    public User()
    {
        this.userMessages = new List<Chat>();
    }
    public User(string fullName, string server, string nickName, string password = "")
    {
        this.userId = fullName + "," + server;
        this.fullName = fullName;
        this.password = password;
        this.nickName = nickName;
        this.server = server;
        this.userMessages = new List<Chat>();
        last = "";
        lastDate = DateTime.Now; 
    }
    public bool chackPassword(string pass)
    {
        if (pass == null) return false;
        return password.Equals(pass);
    }
}

