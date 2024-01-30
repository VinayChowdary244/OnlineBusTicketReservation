namespace BusTicketingWebApplication.Models
{
    public class UpcomingJourney
    {
        
        public int BookingId { get; set; }

        public string UserName { get; set; }
        public int BusId { get; set; }
        public string? Date { get; set; }
        public List<int> SelectedSeats { get; set; }
        //public int NoOfSeats { get; set; }
        public float TotalFare { get; set; }

        //public string? SrartTime  { get; set; }
        //public string? Duration { get; set; }

    }
}
