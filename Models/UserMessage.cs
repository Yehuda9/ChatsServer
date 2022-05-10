
    public class UserMessage
    {
    public int Id { get; set; }
    public string fromId { get; set; }
    public string toId { get; set; } 
    /*public User from { get; set; }
    public User to { get; set; }*/
    public Message message { get; set; }
    public string messageId { get; set; }
    public List<User> contacts { get; set; }

}

