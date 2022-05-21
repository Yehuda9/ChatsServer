using Microsoft.EntityFrameworkCore;

public class MessagesServiceM : MessagesIService
{
    DataContext context = new();

    public void addMessage(string from, string to, string content, FileModel? fileModel = null)
    {
        var chat = context.chats.Where(c => (c.user1Id == from && c.user2Id == to) || (c.user1Id == to && c.user2Id == from)).First();
        context.messages.Add(new Message(from, to, content, chat.id,fileModel));
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
        return context.messages.Include(m=>m.formFile).Where(c => (c.fromId == user && c.toId == contact) || (c.fromId == contact && c.toId == user)).ToList();
    }

    public void updateMessage(string user, string contact, string msg, string content)
    {
        getMessage(user, contact, msg).content = content;
        context.SaveChanges();
    }
    public Chat? getChatByName(string userId, string contactName)
    {
        var chats = context.chats;
        var userChats = chats.Where(x => x.user1Id == userId || x.user2Id == userId).ToList();
        return userChats.Find((c) => (c.user1Id.Split(",")[0] == contactName || c.user2Id.Split(",")[0] == contactName));
    }
}

