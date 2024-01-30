using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBookedSeatService
    {
        public List<int> GetSeatsById(BusIdDTO busIdDTO);
    }
}
