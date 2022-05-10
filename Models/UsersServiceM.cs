public class UsersServiceM : UsersIService
{
    private readonly DataContext context = new();

    public void addContact(string id, string contact)
    {
        context.chats.Add(new Chat(id, contact));
        context.SaveChanges();
    }

    public bool checkPassword(User user, string password)
    {
        if (user != null && user.password == password)
        {
            return true;
        }
        return false;
    }

    public void create(string fullName,string nickName, string server, string password = "")
    {
        var user = new User(fullName, server,nickName, password);
        if (!context.users.Contains(user))
        {
            context.Add(user);
            context.SaveChanges();
        }
    }


    public void delete(string id, string server)
    {
        User? user = context.users.Find(id+","+server);
        if (user != null)
        {
            context.Remove(user);
            context.SaveChanges();

        }
    }

    public User? get(string id, string server)
    {
        return context.users.Find(id + "," + server);
    }

    public List<User> getAllContacts(string id)
    {
        var t = context.chats;
        var t1 = t.Where(x=>x.id.Contains(id)).ToList();
        var t2=new List<User>();
        foreach (var x in t1)
        {
            if (x.user1Id != id)
            {
                t2.Add(context.users.Find(x.user1Id));
            }
            if (x.user2Id != id)
            {
                t2.Add(context.users.Find(x.user2Id));
            }
        }
        return t2;
    }

    public User? getContact(string id, string contact)
    {
        return getAllContacts(id).Find(x => x.userId == contact);
    }

    public void removeContact(string id, string contact)
    {
        //context.users.Find(id).userMessages.Remove(co);
    }

    public void update(string id, string name, string password, string server = "")
    {
        User? user = context.users.Find(id);
        if (user != null)
        {
            user.nickName = name;
            user.password = password;
        }
        context.SaveChanges();

    }
}

