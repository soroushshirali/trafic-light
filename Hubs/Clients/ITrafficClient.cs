using System.Threading.Tasks;
using traffic_light.Models;

namespace traffic_light.Hubs.Clients
{
    public interface ITrafficClient
    {
        Task ReceiveNewStatus(LightsStatusDto status);
    }
}