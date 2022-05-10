public interface ContactsIService
{
    List<User> getAll(string user);
    User get(string user,string id);
    void delete(string user,String id);
    void update(string user,User contact);
    void create(string user,User contact);
    void addMessage(string contact,string content);
    void editMessage(string contact,int id, string content);
}
