[Serializable]
public class Contact

{
    public Contact(string id,string name,string server)
    {
        this.id = id;
        this.name = name;
        this.chats=new List<Chat>();
        this.server = server;
        this.last = "";
        this.lastdate = new DateTime();
    }
    public string? id { get; set; }
    public string? name { get; set; }
    public string? server { get; set; }
    public List<Chat> chats { get; set; }
    public string last { get; set; }
    public DateTime lastdate { get; set; }
    

}

