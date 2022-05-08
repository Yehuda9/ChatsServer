
public class Message
{

    public int id { get; set; }
    public DateTime created { get; set; }
    public string? content { get; set; }
    public bool sent { get; set; }
    public Message(int id,string content)
    {
        this.id = id;
        this.content = content;
        this.sent = false;
        this.created = DateTime.Now;
    }
}

