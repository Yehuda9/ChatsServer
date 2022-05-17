
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(Message message)
    {
        await Clients.User(message.toId).ReceiveMessage(message);
        //await Clients.All.ReceiveMessage(message);
    }
}

