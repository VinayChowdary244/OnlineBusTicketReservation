namespace BusTicketingWebApplication.Models
{
    public class BookedSeat
    {
        public int Id { get; set; }
        public List<int> BookedSeats { get; set; }
        public int BusId { get; set; }
        public string Date { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeatCount { get; set; }
    }
}
