public interface ContactsIService
{
    IReadOnlyCollection<Contact> getAll(User user);
    Contact get(User user,string id);
    void delete(User user,String id);
    void update(User user,Contact contact);
    void create(User user,Contact contact);
    void addMessage(Contact contact,string content);
    Contact getContact(User user, string id);
}
