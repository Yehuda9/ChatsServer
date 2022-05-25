
public interface UsersIService
{
    public void create(string fullName,string nickName,string server, IFormFile? img=null, string password="");
    public void update(string id, string name, string password="",string server="");
    public void delete(string name,string server);
    public User? get(string name,string server);
    public User? get(string id);

    public List<User> getAllContacts(string id);
    public User? getContact(string id,string contact);
    public void addContact(string id,string contact);
    public void removeContact(string id,string contact);
    public bool checkPassword(User user,string password);
}

