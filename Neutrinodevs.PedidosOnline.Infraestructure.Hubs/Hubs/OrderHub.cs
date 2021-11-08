using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Neutrinodevs.PedidosOnline.Infraestructure.Hubs.Hubs
{
    public class OrderHub : Hub
    {
        public async Task NotifyOrder()
        {
            await Clients.All.SendAsync("ReceiveNotifyOrder");
        }
    }
}
