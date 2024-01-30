using BusModelLibrary;
using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBookingRepository
    {
        public Booking Add(Booking item);
        public Booking Delete(int key);
        public Booking GetById(int key);
        public IList<Booking> GetAll();
        public Booking Update(Booking item);
    }
}

