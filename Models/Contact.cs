using System.ComponentModel.DataAnnotations;

[Serializable]
public class Contact

{
    public Contact(string id,string name="",string server="")
    {
        this.id = id;
        this.name = name;
        this.chats=new List<Chat>();
        this.server = server;
        this.last = "";
        this.lastdate = new DateTime();
    }
    [Key]
    public string? id { get; set; }
    public string name { get; set; }
    public string server { get; set; }
    public List<Chat> chats { get; set; }
    public string last { get; set; }
    public DateTime lastdate { get; set; }
    public override bool Equals(Object? obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Contact c = (Contact)obj;
            return this.id==c.id;
        }
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

