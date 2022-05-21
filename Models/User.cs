using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

public class User
{
    [Key]
    public string userId { get; set; }
    [JsonPropertyName("id")]
    public string fullName { get; set; }
    [JsonIgnore]
    public string password { get; set; }
    [JsonPropertyName("name")]
    public string nickName { get; set; }
    public string server { get; set; }
    public string last { get; set; }
    public DateTime lastDate { get; set; }
    public List<Chat> userMessages { get; set; }
   /* [ForeignKey("profileImg")]
    public string profileImgId { get; set; }*/
    public Img profileImg { get; set; } 
    public User()
    {
        this.userMessages = new List<Chat>();
    }
    public User(string fullName, string server, string nickName, string password = "", Img proImg = null)
    {
        this.userId = fullName + "," + server;
        this.fullName = fullName;
        this.password = password;
        this.nickName = nickName;
        this.server = server;
        this.userMessages = new List<Chat>();
        last = "";
        lastDate = DateTime.Now;
        this.profileImg = proImg;
    }
    public bool chackPassword(string pass)
    {
        if (pass == null || this.password == "") return false;
        return password == pass;
    }
}

