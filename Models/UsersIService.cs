
public interface UsersIService
{
    public void create(string id, string name, string password);
    public void update(string id, string name, string password);
    public void delete(string id);
    public User getUserByName(string name);
    public User getUserById(string id);
    public bool checkPassword(User user,string password);
}

