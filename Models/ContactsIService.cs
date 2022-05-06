public interface ContactsIService
{
    IReadOnlyCollection<Contact> getAll();
    Contact get(String id);
    void delete(String id);
    void update(Contact contact);
    void create(Contact contact);

}

