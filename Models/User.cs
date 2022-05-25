using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

public class User
{
    [Key]
    [JsonIgnore]
    public string userId { get; set; }
    [JsonPropertyName("id")]
    public string fullName { get; set; }
    [JsonIgnore]
    public string password { get; set; }
    [JsonPropertyName("name")]
    public string nickName { get; set; }
    public string server { get; set; }
    public string last { get; set; }
    public string lastType { get; set; }

    public DateTime lastDate { get; set; }
    public List<Chat> userMessages { get; set; }
    /* [ForeignKey("profileImg")]
     public string profileImgId { get; set; }*/
    public FileModel profileImg { get; set; }
    public User()
    {
        this.userMessages = new List<Chat>();
    }
    public User(string fullName, string server, string nickName, string password = "", IFormFile? proImg = null)
    {
        this.userId = fullName + "," + server;
        this.fullName = fullName;
        this.password = password;
        this.nickName = nickName;
        this.server = server;
        this.userMessages = new List<Chat>();
        last = "";
        lastType = "text";
        lastDate = DateTime.Now;
        if (proImg != null && proImg.ContentType == "image/jpeg")
        {
            this.profileImg = new(proImg);
        }
        else
        {
            string path = Directory.GetCurrentDirectory();
            var picPath = Path.Join(path, "wwwroot\\generic_profile_image.png");
            byte[] bytes = File.ReadAllBytes(picPath);
            this.profileImg = new FileModel(bytes,"profileImage", "image/jpeg");
        }
    }
    public bool chackPassword(string pass)
    {
        if (pass == null || this.password == "") return false;
        return password == pass;
    }
}

