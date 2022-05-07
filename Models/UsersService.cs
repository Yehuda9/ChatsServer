public class UsersService : UsersIService
{
    private static List<User> usersList = new List<User>();

    public bool checkPassword(User user, string password)
    {
        if (user != null && user.password == password)
        {
            return true;
        }
        return false;   
    }

    public void create(string id, string name, string password)
    {
        User user = new User(id, password, name);
        if (!usersList.Contains(user))
        {
            usersList.Add(user);
        }
    }

    public void delete(string id)
    {
        User user = usersList.Find(x => x.id == id);
        if (user != null)
        {
            usersList.Remove(user);
        }
    }

    public User getUserById(string id)
    {
        return usersList.Find(x => x.id == id);
    }

    public User getUserByName(string name)
    {
        return usersList.Find(x => x.name == name);
    }

    public void update(string id, string name, string password)
    {
        User user = usersList.Find(x => x.id == id);
        if (user != null)
        {
            user.name = name;
            user.password = password;
        }
    }
}

