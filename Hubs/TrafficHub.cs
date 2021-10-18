using traffic_light.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace traffic_light.Hubs
{
    public class TrafficHub : Hub<ITrafficClient>
    { }
}