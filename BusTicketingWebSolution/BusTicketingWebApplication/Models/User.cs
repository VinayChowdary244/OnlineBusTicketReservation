using System.ComponentModel.DataAnnotations;

namespace BusTicketingWebApplication.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Email { get; set; }
      
        public string Phone { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public byte[] Password { get; set; }
        public string Role { get; set; }
        public byte[] Key { get; set; }

    }
}
