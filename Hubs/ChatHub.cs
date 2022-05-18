using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


//[Authorize]
public class ChatHub : Hub
{
    public static Dictionary<string,string> idToConnection=new();
    /*public ChatHub()
    {
        idToConnection = new Dictionary<string,string>();
    }*/

    public override Task OnConnectedAsync()
    {
        // Add your own code here.
        // For example: in a chat application, record the association between
        // the current connection ID and user name, and mark the user as online.
        // After the code in this method completes, the client is informed that
        // the connection is established; for example, in a JavaScript client,
        // the start().done callback is executed.
        string? name = getName();
        var ConnectionID = Context.ConnectionId;
        idToConnection[name] = ConnectionID;
        return base.OnConnectedAsync();
    }
    private string? getName()
    {
        return Context.User.Claims.SingleOrDefault(x => x.Type.EndsWith("name"))?.Value;
    }
    public async Task SendMessage(string id)
    {
        //await Clients.All.SendAsync("ReceiveMessage");

        await Clients.Clients(idToConnection[id.Split(',')[0]]).SendAsync("ReceiveMessage");
        //await Clients.User(from).ReceiveMessage(,to, content);

        // await Clients.User(to).ReceiveMessage(to, content);
        //await Clients.All.ReceiveMessage(message);
        //return null;
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

