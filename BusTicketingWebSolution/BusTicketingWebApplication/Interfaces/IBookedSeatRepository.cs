using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBookedSeatRepository
    {
        public BookedSeat Add(BookedSeat item);
        public BookedSeat Delete(int key);
        public BookedSeat GetById(int key);
        public IList<BookedSeat> GetAll();
        public BookedSeat Update(BookedSeat item);
    }
}
