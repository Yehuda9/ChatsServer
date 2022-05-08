using System.ComponentModel.DataAnnotations;

[Serializable]
public class Contact

{
    public Contact(string id, string name = "", string server = "")
    {
        this.id = id;
        this.name = name;
        this.messages = new List<Message>();
        this.server = server;
        this.last = "";
        this.lastdate = new DateTime();
    }
    [Key]
    public string? id { get; set; }
    public string name { get; set; }
    public string server { get; set; }
    public List<Message> messages { get; set; }
    public string last { get; set; }
    public DateTime lastdate { get; set; }
    public void addMessage(string content)
    {
        messages.Add(new Message(messages.Capacity + 1, content));
    }
    public void editMessage(int id, string content)
    {
        Message? message = messages.Find(x => x.id == id);
        if (message == null) { return; }
        message.content = content;
    }
    public void deleteMessage(int id, string content)
    {
        Message? message = messages.Find(x => x.id == id);
        if (message == null) { return; }
        messages.Remove(message);
    }
    public override bool Equals(Object? obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Contact c = (Contact)obj;
            return this.id == c.id;
        }
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

