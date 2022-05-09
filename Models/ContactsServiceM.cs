public class ContactsServiceM : ContactsIService
{
    private readonly DataContext context = new();

    public void addMessage(string contact, string content)
    {

    }

    public void create(string user, Contact contact)
    {

        if (context.users.Find(user) != null)
        {
            
            context.contacts.Where(c => c.UseridName == user);
            context.users.Find(user).contacts.Add(contact);
        }
    }

    public void delete(string user, string id)
    {
       /* Contact c = context.users.Find(user).contacts.Find(id);
        if (c != null) { user.contacts.Remove(c);
    }*/
    }

    public void editMessage(string contact, int id, string content)
    {
        //contact.editMessage(id, content);
    }

    public Contact get(string user, string id)
    {
        return null;
        //return user.contacts.Find(x => x.id == id);
    }

    public IReadOnlyCollection<Contact> getAll(string user)
    {
        return context.users.Find(user).contacts;
        //return user.contacts.AsReadOnly();
    }

    public Contact getContact(string user, string id)
    {
        return null;
        //return user.GetContact(id);
    }

    public void update(string user, Contact contact)
    {
        /*Contact c = user.contacts.Find(x => x.id == contact.id);

        if (c != null)
        {
            c.name = contact.name;
            c.server = contact.server;
        }*/
    }
}

