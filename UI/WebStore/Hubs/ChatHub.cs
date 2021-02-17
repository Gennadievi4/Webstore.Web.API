using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebStore.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string Message)
        {
            await Clients.All.SendAsync("MessageFromClient", Message);
        }
    }
}
