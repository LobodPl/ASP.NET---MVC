using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebApplication2.Hubs
{
    public class CounterHub : Hub
    {
        private static int counter = 0;

        public override async Task OnConnectedAsync()
        {
            counter++;
            await Clients.All.SendAsync("UserCount", counter);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            counter--;
            await Clients.All.SendAsync("UserCount", counter);
        }
    }
}