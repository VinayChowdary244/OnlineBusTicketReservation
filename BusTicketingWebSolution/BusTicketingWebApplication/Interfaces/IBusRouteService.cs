using BusModelLibrary;
using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBusRouteService
    {
        List<BusRoute> GetRoutes();
        BusRoute Add(BusRoute busroute);
    }
}
