public interface ContactsIService
{
    IReadOnlyCollection<ContactModel> getAll(string userId);
    ContactModel get(string userId, string contactId);
    void delete(string userId, string contactId);
    void update(string userId, string contactId);
    void create(string userId, string contactId);
    /*void addMessage(Contact contact, string content);
    void editMessage(Contact contact, int id, string content);
*/
}
