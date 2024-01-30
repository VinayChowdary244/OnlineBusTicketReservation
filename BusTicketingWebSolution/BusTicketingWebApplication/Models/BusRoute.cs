using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketingWebApplication.Models
{
    public class BusRoute
    {
        [Key]
        public int  RouteId { get; set; }
        public string Start { get; set;}
        public string End { get; set;}

        [ForeignKey("BusId")]
        public int Bus { get; set; }


    }
}
