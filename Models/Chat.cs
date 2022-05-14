
public class Chat
{
    public string id { get; set; }
    public string user1Id { get; set; }
    public string user2Id { get; set; }
    public List<User> users { get; set; }
    public List<Message> messages { get; set; }
    public Chat() { }
    public Chat(string id1, string id2)
    {
        user1Id = id1;
        user2Id = id2;
        users = new List<User>();
        messages = new List<Message>();
        id = user1Id + ":" + user2Id;
    }
}

