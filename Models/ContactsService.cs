
public class ContactsService : ContactsIService
{
    private static List<Contact> contactsList = new List<Contact>();
    public void create(Contact contact)
    {
        if (!contactsList.Contains(contact))
        {
            contactsList.Add(contact);
        }
    }

    public void delete(string id)
    {
        Contact c = contactsList.Find(x => x.id == id);
        if (c != null) { contactsList.Remove(c); }
    }

    public Contact get(string id)
    {
        return contactsList.Find(x => x.id == id);
    }

    public IReadOnlyCollection<Contact> getAll()
    {
        return contactsList.AsReadOnly();
    }

    public void update(Contact contact)
    {
        Contact c = contactsList.Find(x => x.id == contact.id);

        if (c != null)
        {
            c.name = contact.name;
            c.server = contact.server;
        }
    }
}

