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
        if (user != null && user.chackPassword(password))
        {
            return true;
        }
        return false;
    }

    public void create(string fullName, string nickName, string server, string password = "")
    {
        var user = new User(fullName, server, nickName, password);
        if (!context.users.Contains(user))
        {
            context.Add(user);
            context.SaveChanges();
        }
    }


    public void delete(string id, string server)
    {
        User? user = context.users.Find(id + "," + server);
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
    private string findLastMsg(string userId, string contactId)
    {
        var chatId = context.chats.Where(x => x.id.Contains(contactId) && x.id.Contains(userId)).FirstOrDefault().id;
        var res = context.messages.Where(m => m.chatId == chatId);
        var res1 = res.OrderBy(m => m.created).ToList();
        if (res1.Capacity == 0) return "";
        var res2 = res1.Last().content;
        return res2 != null ? res2 : "";
    }
    public List<User> getAllContacts(string userId)
    {
        var t = context.chats;
        var t1 = t.Where(x => x.id.Contains(userId)).ToList();
        var t2 = new List<User>();
        foreach (var x in t1)
        {
            if (x.user1Id != userId)
            {
                var u = context.users.Find(x.user1Id);
                u.last = findLastMsg(userId, u.userId);
                t2.Add(u);
            }
            if (x.user2Id != userId)
            {
                var u = context.users.Find(x.user2Id);
                u.last = findLastMsg(userId, u.userId);
                t2.Add(u);
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

