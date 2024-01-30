using BusModelLibrary;
using BusTicketingWebApplication.Models.DTOs;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBusService
    {
        List<Bus> GetBuses();
        Bus Add(Bus bus);
        BusIdDTO RemoveBus( BusIdDTO busIdDTO);
        BusDTO UpdateBus(BusDTO busDTO);
        Bus GetBusById(BusIdDTO busIdDTO);
    }
}
