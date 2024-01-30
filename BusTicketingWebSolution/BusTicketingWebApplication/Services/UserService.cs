using BusModelLibrary;  // Importing the BusModelLibrary namespace for Bus-related classes
using BusTicketingWebApplication.Exceptions;  // Importing custom exception classes
using BusTicketingWebApplication.Interfaces;  // Importing interfaces for dependency injection
using BusTicketingWebApplication.Models;  // Importing models for user and DTOs
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Repositories;  // Importing repositories for data access
using System.Security.Cryptography;  // Importing classes for cryptographic operations
using System.Text;

namespace BusTicketingWebApplication.Services
{
    // UserService class responsible for user-related operations
    public class UserService : IUserService
    {
        // Dependencies injected via constructor
        private readonly IUserRepository _userrepository;
        private readonly IBusRepository _busrepository;
        private readonly ITokenService _tokenService;
        private readonly IBookingRepository _bookingRepository;

        // Constructor with full set of dependencies
        public UserService(IUserRepository userrepository, ITokenService tokenService, IBusRepository busrepository, IBookingRepository bookingRepository)
        {
            _userrepository = userrepository;
            _tokenService = tokenService;
            _busrepository = busrepository;
            _bookingRepository = bookingRepository;
        }

        // Constructor with minimal dependencies
        public UserService(IUserRepository userrepository, ITokenService tokenService)
        {
            _userrepository = userrepository;
            _tokenService = tokenService;
        }

        // Method for user login
        public UserDTO Login(UserDTO userDTO)
        {
            // Retrieve user from repository
            var user = _userrepository.GetById(userDTO.UserName);

            // Check if user exists
            if (user != null)
            {
                // Validate password using HMACSHA512
                HMACSHA512 hmac = new HMACSHA512(user.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (user.Password[i] != userpass[i])
                        return null;
                }

                // Generate JWT token and update DTO
                userDTO.Token = _tokenService.GetToken(userDTO);
                userDTO.Password = "";
                userDTO.Email = user.Email;
                return userDTO;
            }

            return null;  // Return null if user does not exist
        }

        // Method for user registration
        public UserDTO Register(UserDTO userDTO)
        {
            // Create HMACSHA512 for password hashing
            HMACSHA512 hmac = new HMACSHA512();

            // Create User object with hashed password and other details
            User user = new User()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                City = userDTO.City,
                Pincode = (int)userDTO.Pincode,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                Key = hmac.Key,
                Role = userDTO.Role
            };

            // Add user to repository and return DTO
            var result = _userrepository.Add(user);
            if (result != null)
            {
                userDTO.Password = "";
                return userDTO;
            }

            return null;  // Return null if user registration fails
        }

        // Method to update user information
        public UserDataDTO UpdateUser(UserDataDTO userDataDTO)
        {
            // Retrieve user from repository
            var userData = _userrepository.GetById(userDataDTO.UserName);

            // Update user details
            userData.Email = userDataDTO.Email;
            userData.City = userDataDTO.City;
            userData.Phone = userDataDTO.Phone;
            userData.Pincode = userDataDTO.Pincode;

            // Check if update is successful
            if (userData != null)
            {
                var res = _userrepository.Update(userData);
                if (res != null)
                {
                    return userDataDTO;
                }
            }
            throw new NoUsersAvailableException();  // Throw exception if user not found
        }

        // Method to get a list of all buses
        public List<Bus> GetAll()
        {
            var busses = _busrepository.GetAll();
            if (busses != null)
            {
                return busses.ToList();
            }
            throw new NoBusesAvailableException();  // Throw exception if no buses found
        }

        // Method to search for buses based on start and end locations
        public List<Bus> BusSearch(BusSearchDTO busSearchDto)
        {
            var search = _busrepository.GetAll();
            if (search != null)
            {
                List<Bus> BusList = new List<Bus>();

                // Iterate through buses and add matching ones to the list
                for (int i = 0; i < search.Count; i++)
                {
                    if (search[i].Start == busSearchDto.Start && search[i].End == busSearchDto.End)
                    {
                        BusList.Add(search[i]);
                    }
                }

                // Check if any buses were found
                if (BusList.Count > 0)
                    return BusList;
                else
                    throw new NoBusesAvailableException();  // Throw exception if no matching buses found
            }

            return null;
        }

        // Method to get a list of all users
        public List<User> GetAllUsers()
        {
            var users = _userrepository.GetAll();
            if (users != null)
            {
                return users.ToList();
            }
            throw new NoUsersAvailableException();  // Throw exception if no users found
        }

        // Method to get booking history for a user
        public List<Booking> GetBookingHistory(UserNameDTO userNameDTO)
        {
            var booking = _bookingRepository.GetAll();
            if (booking != null)
            {
                List<Booking> BookingHistory = new List<Booking>();

                // Iterate through bookings and add matching ones to the list
                for (int i = 0; i < booking.Count; i++)
                {
                    if (booking[i].UserName == userNameDTO.UserName)
                    {
                        BookingHistory.Add(booking[i]);
                    }
                }

                // Check if any bookings were found
                if (BookingHistory.Count > 0)
                    return BookingHistory;
                else
                    throw new NoBookingsYetException();  // Throw exception if no bookings found
            }

            return null;
        }
    }
}
