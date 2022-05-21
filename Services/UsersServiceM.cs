public class UsersServiceM : UsersIService
{
    private readonly DataContext context = new();

    public void addContact(string userId, string contactId)
    {
        if (getContact(userId, contactId) == null)
        {
            context.chats.Add(new Chat(userId, contactId));
            context.SaveChanges();
        }

    }

    public bool checkPassword(User user, string password)
    {
        if (user != null && user.chackPassword(password))
        {
            return true;
        }
        return false;
    }

    public void create(string fullName, string nickName, string server, Img img=null, string password = "")
    {
        var user = new User(fullName, server, nickName, password,img);
        if (get(fullName,server)==null)
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
   
    private Chat? getChatByIds(string userId, string contactId)
    {
        var chats = context.chats;
        var userChats = chats.Where(x => x.user1Id == userId || x.user2Id == userId).ToList();
        return userChats.Find(c => c.user1Id == contactId || c.user2Id == contactId);
    }
    private void findLastMsg(User user, string contactId)
    {
        var chatId = context.chats.Where(x => (x.user1Id == contactId && x.user2Id == user.userId) || (x.user2Id == contactId && x.user1Id == user.userId)).FirstOrDefault().id;
        var chat = context.messages.Where(m => m.chatId == chatId);
        var Ochat = chat.OrderBy(m => m.created).ToList();
        if (Ochat.Capacity == 0)
        {
            user.last = "";
            return;
        }
        var lastMsg = Ochat.Last().content;
        var lastDate = Ochat.Last().created;
        user.last = lastMsg != null ? lastMsg : "";
        user.lastDate = lastDate;

    }
    public List<User> getAllContacts(string userId)
    {
        var chats = context.chats;
        var userChats = chats.Where(x => x.user1Id == userId || x.user2Id == userId).ToList();
        var result = new List<User>();
        foreach (var chat in userChats)
        {
            if (chat.user1Id != userId)
            {
                var contact = context.users.Find(chat.user1Id);
                findLastMsg(contact, userId);
                result.Add(contact);
            }
            if (chat.user2Id != userId)
            {
                var contact = context.users.Find(chat.user2Id);
                findLastMsg(contact, userId);
                result.Add(contact);
            }
        }
        return result;
    }

    public User? getContact(string id, string contactId)
    {
        return getAllContacts(id).Find(usr => usr.userId == contactId);
    }

    public void removeContact(string id, string contactId)
    {
        var chat = getChatByIds(id, contactId);
        context.chats.Remove(chat);
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

