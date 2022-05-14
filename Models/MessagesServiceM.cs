
public class MessagesServiceM : MessagesIService
{
    DataContext context = new();

    public void addMessage(string from, string to, string content)
    {
        var t = context.chats.Where(c=> (c.user1Id == from && c.user2Id == to) || (c.user1Id == to && c.user2Id == from)).First();
        context.messages.Add(new Message(from, to, content,t.id));
        /*var t = context.chats;
        var t1 = t.Where(c => (c.user1Id == from && c.user2Id == to) || (c.user1Id == to && c.user2Id == from));
        var t2 = t1.First();
        var t3 = t2.messages;
        t3.Add(new Message(from, to, content));*/

        //context.chats.Where(c => (c.user1Id == from && c.user2Id == to) || (c.user1Id == to && c.user2Id == from)).First().messages.Add(new Message(from, to, content));
        context.SaveChanges();
    }

    public void deleteMessage(string user, string contact, string msg)
    {
        context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages.Remove(getMessage(user, contact, msg));
        context.SaveChanges();

    }

    public Message? getMessage(string user, string contact, string msg)
    {
        return context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages.Find(x => x.MessageId == msg);

    }

    public List<Message> getMessages(string user, string contact)
    {
        return context.messages.Where(c => (c.fromId == user && c.toId == contact) || (c.fromId == contact && c.toId == user)).ToList();
        //return context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages;
    }

    public void updateMessage(string user, string contact, string msg, string content)
    {
        getMessage(user, contact, msg).content = content;
        context.SaveChanges();

    }
}

