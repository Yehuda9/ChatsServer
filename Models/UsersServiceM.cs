public class UsersServiceM : UsersIService
{
    private readonly DataContext context = new();

    public void addContact(string userId, string contactId)
    {
        context.chats.Add(new Chat(userId, contactId));
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


    public void delete(string userId, string server)
    {
        User? user = context.users.Find(userId + "," + server);
        if (user != null)
        {
            context.Remove(user);
            context.SaveChanges();

        }
    }

    public User? get(string userId, string server)
    {
        return context.users.Find(userId + "," + server);
    }
    private string findLastMsg(string userId, string contactId)
    {
        var chatId = context.chats.Where(x => x.id.Contains(contactId) && x.id.Contains(userId)).FirstOrDefault().id;
        var chat = context.messages.Where(m => m.chatId == chatId);
        var Ochat = chat.OrderBy(m => m.created).ToList();
        if (Ochat.Capacity == 0) return "";
        var lastMsg = Ochat.Last().content;
        return lastMsg != null ? lastMsg : "";
    }
    public List<User> getAllContacts(string userId)
    {
        var chats = context.chats;
        var userChats = chats.Where(x => x.id.Contains(userId)).ToList();
        var result = new List<User>();
        foreach (var chat in userChats)
        {
            if (chat.user1Id != userId)
            {
                var usr = context.users.Find(chat.user1Id);
                usr.last = findLastMsg(userId, usr.userId);
                result.Add(usr);
            }
            if (chat.user2Id != userId)
            {
                var usr = context.users.Find(chat.user2Id);
                usr.last = findLastMsg(userId, usr.userId);
                result.Add(usr);
            }
        }
        return result;
    }
    
    public Chat? getChatByName(string userID, string contactName)
    {
        var chats = context.chats;
        var userChats = chats.Where(x => x.id.Contains(userID)).ToList();
        return userChats.Find((c) => (c.user1Id.Contains(contactName) || c.user2Id.Contains(contactName)));
    }

    public User? getContact(string id, string contactId)
    {
        return getAllContacts(id).Find(usr => usr.userId == contactId);
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

