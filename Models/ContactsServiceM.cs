using Microsoft.EntityFrameworkCore;

public class ContactsServiceM : ContactsIService
{
    private readonly DataContext context = new();

    public void addMessage(string contact, string content)
    {

    }

    public void create(string user, User contact)
    {
        contact.userMessages.Add(new Chat());
        context.Add(contact);
        /*if (context.users.Find(user) != null)
        {
            User? u = context.users.Where(p => p.userId == user && p.server == "").Include("contacts").FirstOrDefault();
            //u.contacts.Add(contact);
        }*/
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

    public User get(string user, string id)
    {
        return null;
        //return user.contacts.Find(x => x.id == id);
    }

    public List<User> getAll(string user)
    {
        User? u = context.users.Where(p => p.userId == user && p.server == "").Include("contacts").FirstOrDefault();
        return null;
        //return context.users.Where(p=>u.contacts.Contains(p.userId)).ToList();
    }

    public User getContact(string user, string id)
    {
        return null;
        //return user.GetContact(id);
    }

    public void update(string user, User contact)
    {
        /*Contact c = user.contacts.Find(x => x.id == contact.id);

        if (c != null)
        {
            c.name = contact.name;
            c.server = contact.server;
        }*/
    }
}

