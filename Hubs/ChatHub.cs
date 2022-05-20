using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


[Authorize]
public class ChatHub : Hub
{
    public readonly static Dictionary<string,string> idToConnection=new();

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

    public async Task SendMessage(string from,string to,string message)
    {
        if (!idToConnection.ContainsKey(to.Split(',')[0])) { return; }
        await Clients.Clients(idToConnection[to.Split(',')[0]]).SendAsync("ReceiveMessage" , from, to, message);
    }
}

