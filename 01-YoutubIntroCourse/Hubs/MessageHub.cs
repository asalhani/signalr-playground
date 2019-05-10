

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalrDemo
{
    public class MessageHub : Hub
    {
        public Task SendMessageToAllClient(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            // notify all existing connections about the new connection
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
                 // notify all existing connections about the new connection
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}