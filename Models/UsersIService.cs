
public interface UsersIService
{
    public void create(string name,string server, string password="");
    public void update(string id, string name, string password);
    public void delete(string id);
    public User get(string id);
    public bool checkPassword(User user,string password);
}

