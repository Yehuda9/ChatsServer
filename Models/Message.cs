
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    [Key]
    public string MessageId { get; set; }
    public DateTime created { get; set; }
    public string? content { get; set; }
    public FileModel? formFile { get; set; }
    public bool sent { get; set; }
    public string fromId { get; set; }
    public string toId { get; set; }
    public string chatId { get; set; }
    public Message()
    {

    }
    public Message(string from, string to, string content, string chatId, FileModel? formFile=null)
    {
        fromId = from;
        toId = to;
        this.content = content;
        created = DateTime.Now;
        MessageId = from + "," + to + "," + created + "," + content;
        sent = false;
        this.chatId = chatId;
        this.formFile = formFile;
    }
}

