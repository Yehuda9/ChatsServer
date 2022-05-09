public class UsersServiceM : UsersIService
{
    private readonly DataContext context = new();
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
        
        User? user = new(id, password, name);
        if (!context.users.Contains(user))
        {
            context.users.Add(user);
            context.SaveChanges();

        }
    }

    public void delete(string id)
    {
        User? user = context.users.Find(id);
        if (user != null)
        {
            context.Remove(user);
        }
    }

    public User? get(string id)
    {
        return context.users.Find(id);
    }

    public void update(string id, string name, string password)
    {
        User? user = context.users.Find(id);
        if (user != null)
        {
            user.nickName = name;
            user.password = password;
        }
    }
}

