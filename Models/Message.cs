
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public DateTime created { get; set; }
    public string? content { get; set; }
    public bool sent { get; set; }
    public string fromId;
    public string toId;
    public Message(string content, string from, string to)
    {
        this.content = content;
        this.sent = false;
        this.created = DateTime.Now;
        this.fromId = from;
        this.toId = to;
    }
}

