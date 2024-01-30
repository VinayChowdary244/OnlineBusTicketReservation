using BusModelLibrary;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IUserService
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserDTO userDTO);
        UserDataDTO UpdateUser(UserDataDTO userDataDTO);
        List<Bus> BusSearch(BusSearchDTO busSearchDTO);
       
        List<Booking> GetBookingHistory(UserNameDTO userNameDTO);
        List<User> GetAllUsers();
        
    }
}
