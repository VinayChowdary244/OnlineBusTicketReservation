using System.ComponentModel.DataAnnotations;

namespace BusTicketingWebApplication.Models.DTOs
{
    public class BusDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Cost { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }



        public string Start { get; set; }
        public string End { get; set; }
    }
}
