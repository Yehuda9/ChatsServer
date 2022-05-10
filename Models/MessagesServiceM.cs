
public class MessagesServiceM : MessagesIService
{
    DataContext context = new();

    public void addMessage(string from, string to, string content)
    {
        context.chats.Where(c => (c.user1Id == from && c.user2Id == to) || (c.user1Id == to && c.user2Id == from)).ToList().FirstOrDefault().messages.Add(new Message(from, to, content));
    }

    public void deleteMessage(string user, string contact, string msg)
    {
        context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages.Remove(getMessage(user, contact, msg));
    }

    public Message? getMessage(string user, string contact, string msg)
    {
        return context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages.Find(x => x.MessageId == msg);

    }

    public List<Message> getMessages(string user, string contact)
    {
        return context.chats.Where(c => (c.user1Id == user && c.user2Id == contact) || (c.user1Id == contact && c.user2Id == user)).ToList().FirstOrDefault().messages;
    }

    public void updateMessage(string user, string contact, string msg, string content)
    {
        getMessage(user, contact, msg).content = content;
    }
}

