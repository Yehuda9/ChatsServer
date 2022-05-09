using Microsoft.EntityFrameworkCore;

public class ContactsService : ContactsIService
{
    private readonly DataContext db;

    public ContactsService(DataContext d)
    {
        db = d;
    }
    /*public void addMessage(Contact contact, string content)
    {
        contact.addMessage(content);
    }*/

    public void create(string userId, string contactId,string server)
    {

        if (db.chats.Find(userId, contactId)==null)
        {
            db.chats.Add(new Chat(userId, contactId));
            /*db.users.First<User>(s => s.idName==userId);

            db.users.Find(user => user. == userId);
            db.users.Find(userId).Add(new ContactModel(contactId,contactId,server));*/
        }
    }

    public void delete(User user, string id)
    {
        /*Contact c = user.contacts.Find(x => x.id == id);
        if (c != null) { user.contacts.Remove(c); }*/
    }

    public void editMessage(string contact, int id, string content)
    {
        /*db.chats.Find()
        contact.editMessage(id, content);*/
    }

    public User? get(string user, string id)
    {
        if(db.chats.Find(user, id) != null)
        {
            db.users.Find(id);
        }
        return null;
        /*return db.chats.Find(user, id).id2;
        return user.contacts.Find(x => x.id == id);*/
    }

    public IReadOnlyCollection<User> getAll(string user)
    {
        return db.
        return user.contacts.AsReadOnly();
    }

    public Contact getContact(User user, string id)
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

