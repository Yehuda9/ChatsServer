public interface ContactsIService
{
    IReadOnlyCollection<Contact> getAll(string user);
    Contact get(string user,string id);
    void delete(string user,String id);
    void update(string user,Contact contact);
    void create(string user,Contact contact);
    void addMessage(string contact,string content);
    void editMessage(string contact,int id, string content);
}
