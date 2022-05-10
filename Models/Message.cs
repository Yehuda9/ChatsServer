
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    [Key]
    public string MessageId { get; set; }
    public DateTime created { get; set; }
    public string? content { get; set; }
    public bool sent { get; set; }
    public string fromId { get; set; }
    public string toId { get; set; }
    //public User from { get; set; }
    //public User to { get; set; }

    public List<UserMessage> userMessages { get; set; }
    /*public Message() { }
    public Message(string content, string from, string to)
    {
        this.content = content;
        this.sent = false;
        this.created = DateTime.Now;
        this.fromId = from;
        this.toId = to;
        this.MessageId = fromId + "," + toId;
    }*/
}

