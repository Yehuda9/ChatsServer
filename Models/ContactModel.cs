
public class ContactModel
{
    public string id { get; set; }
    public string name { get; set; }
    public string server { get; set; }
    public string last { get; set; }
    public DateTime lastdate { get; set; }
    public List<string> messagesId { get; set; }    
    public ContactModel(string id,string name,string server)
    {
        this.id = id;   
        this.name = name;
        this.server = server;  
        this.last = "";   
        this.lastdate = new();
        messagesId = new List<string>();
        
}

