

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalrDemo
{
    public class MessageHub : Hub
    {
        public  Task SendMessageToAllClient(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}