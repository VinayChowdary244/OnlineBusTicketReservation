using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface ICancelledBookingRepository
    {
        public CancelledBooking Add(CancelledBooking item);
        public CancelledBooking Delete(int key);
        public CancelledBooking GetById(int key);
        public IList<CancelledBooking> GetAll();
        public CancelledBooking Update(CancelledBooking item);
    }
}