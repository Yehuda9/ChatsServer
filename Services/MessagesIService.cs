public interface MessagesIService
{
    public void addMessage(string from, string to, string? content, FileModel? fileModel=null);
    public List<Message> getMessages(string user,string contact);
    public Message? getMessage(string user, string contact, string msg);
    public void updateMessage(string user, string contact, string msg, string content);
    public void deleteMessage(string user, string contact, string msg);
    public Chat? getChatByName(string userID, string contactName);

}