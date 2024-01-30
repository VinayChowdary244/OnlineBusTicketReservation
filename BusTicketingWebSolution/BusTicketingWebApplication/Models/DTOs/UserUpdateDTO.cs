using System.ComponentModel.DataAnnotations;

namespace BusTicketingWebApplication.Models.DTOs
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string UserName { get; set; }


        public string? Email { get; set; }
        public string? Phone { get; set; }

        public string? City { get; set; }

        public int Pincode { get; set; }
    }
}
