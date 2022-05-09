
public class Chat
{
    public List<Message> messages { get; set; }
    public string id1 { get; set; }
    public string id2 { get; set; }
    public Chat(string id1,string id2)
    {
        this.id1 = id1; 
        this.id2 = id2;
        this.messages = new List<Message>();
    }
}

