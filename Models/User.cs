using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public string idName { get; set; }
    [Required]
    public string password { get; set; }
    public string nickName { get; set; }
    public List<string> contactsId { get; set; }

    public User()
    {
    }
    public User(string id,string password,string name)
    {
        this.idName = id;
        this.password = password;   
        this.nickName = name;
        this.contactsId = new List<string>();
    }
}

