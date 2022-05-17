using Microsoft.AspNetCore.SignalR;

public class MyProvider : IUserIdProvider
{

    //private readonly UsersIService? usersIService;
    private readonly static string me = "me";

    /*public MyProvider(UsersIService uis)
    {
        usersIService = uis;
    }*/
    public string GetUserId(HubConnectionContext connection)
    {
        string? name = connection.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        return name+","+me;
        //User? user = usersIService.get(name, me);
        //if (user == null) { return null; }
        //return user.fullName;
    }
    /*private string? getUser()
    {
        string? name = this.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
        if (name == null) { return null; }
        User? user = usersIService.get(name, me);
        if (user == null) { return null; }
        return user.userId;
    }*/
}