public class ContactsService 
{
    private readonly DataContext context = new();

    public void addMessage(Contact contact, string content)
    {
        contact.addMessage(content);
    }

    public void create(User user, Contact contact)
    {
        context.Add(contact);

        if (!user.contacts.Contains(contact))
        {
            user.contacts.Add(contact);
        }
    }

    public void delete(User user, string id)
    {
        Contact c = user.contacts.Find(x => x.id == id);
        if (c != null) { user.contacts.Remove(c); }
    }

    public void editMessage(Contact contact, int id, string content)
    {
        contact.editMessage(id, content);
    }

    public Contact get(User user, string id)
    {
        return user.contacts.Find(x => x.id == id);
    }

    public IReadOnlyCollection<Contact> getAll(User user)
    {
        return user.contacts.AsReadOnly();
    }

    public Contact getContact(User user,string id)
    {
        return user.GetContact(id);
    }

    public void update(User user, Contact contact)
    {
        Contact c = user.contacts.Find(x => x.id == contact.id);

        if (c != null)
        {
            c.name = contact.name;
            c.server = contact.server;
        }
    }
}

