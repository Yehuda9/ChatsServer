﻿/*public class UsersService : UsersIService
{
    private static List<User> usersList = new() { new("y", "1", "y") };
    private readonly DataContext context=new();
    public bool checkPassword(User user, string password)
    {
        if (user != null && user.password == password)
        {
            return true;
        }
        return false;   
    }

    public void create(string id, string name, string password)
    {
        context.Add(new User(id, name, password));
        context.SaveChanges();
        //User? user = usersList.Find(x=>x.idName==id);

        User? user = new(id, password, name);
        if (!usersList.Contains(user))
        {
            usersList.Add(user);
        }
    }

    public void delete(string id)
    {
        User? user = usersList.Find(x => x.name == id);
        if (user != null)
        {
            usersList.Remove(user);
        }
    }

    public User get(string id)
    {
        //return context.users.Find(id);
        return usersList.Find(x => x.name == id);
    }

    public void update(string id, string name, string password)
    {
        User? user = usersList.Find(x => x.name == id);
        if (user != null)
        {
            user.nickName = name;
            user.password = password;
        }
    }
}

*/