
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
    public Message()
    {

    }
    public Message(string from,string to,string content)
    {
        fromId = from;
        toId = to;  
        this.content = content;
        created = DateTime.Now; 
        sent = false;
    }
}

