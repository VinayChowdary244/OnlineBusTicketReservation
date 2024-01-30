using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingWebApplication.Controllers
{
    // CustomerController class for handling customer-related HTTP requests
    [Route("api/[controller]")]  // Route prefix for the controller
    [ApiController]  // Indicates that the controller responds to HTTP API requests
    [EnableCors("reactApp")]  // Enabling CORS for the specified policy ("reactApp")
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;  // Interface for user-related services
        private readonly ILogger<CustomerController> _logger;  // Logger for logging information and errors

        // Constructor to inject dependencies into the controller
        public CustomerController(IUserService userService, ILogger<CustomerController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // POST method to register a new customer
        [HttpPost]
        public ActionResult Register(UserDTO viewModel)
        {
            string message = "";
            try
            {
                var user = _userService.Register(viewModel);  // Call the service to register a new customer
                if (user != null)
                {
                    _logger.LogInformation("Customer is Registered!!");  // Log successful registration
                    return Ok(user);  // Return a 200 OK response with the result
                }
            }
            catch (DbUpdateException exp)
            {
                message = "Duplicate username";
                _logger.LogError("User is not Registered!!");  // Log error for duplicate username
            }
            catch (Exception)
            {
                // Additional exception handling can be added here
            }

            return BadRequest(message);  // Return a 400 Bad Request response with the error message
        }

        // POST method for customer login
        [HttpPost]
        [Route("Login")]  // Attribute-based routing for login
        public ActionResult Login(UserDTO userDTO)
        {
            var result = _userService.Login(userDTO);  // Call the service to perform customer login
            if (result != null)
            {
                _logger.LogInformation("Customers are Logged In!!");  // Log successful login
                return Ok(result);  // Return a 200 OK response with the result
            }
            return Unauthorized("Invalid username or password");  // Return a 401 Unauthorized response
        }

        // POST method for searching buses based on criteria
        [HttpPost]
        [Route("BusSearch")]
        public ActionResult BusSearch(BusSearchDTO busSearchDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.BusSearch(busSearchDTO);  // Call the service to search for buses
                _logger.LogInformation("Bus Search is listed!!");  // Log successful bus search
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Bus Search is not Listed!!");  // Log error for bus search
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // POST method for retrieving booking history of a user
        [HttpPost]
        [Route("UserBookingHistory")]
        public ActionResult BookingHistory(UserNameDTO userIdDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetBookingHistory(userIdDTO);  // Call the service to get booking history
                _logger.LogInformation("User Booking History is Fetched!!");  // Log successful booking history retrieval
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("User Booking History is not Fetched!!");  // Log error for booking history retrieval
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // GET method for retrieving all users (requires "Admin" role)
        [Authorize(Roles = "Admin")]  // Authorization attribute specifying only users with the "Admin" role can access
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetAllUsers();  // Call the service to get a list of all users
                _logger.LogInformation("Users listed");  // Log successful user listing
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("No Such Users are present in the collection or in the table");  // Log error for user listing
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // PUT method for updating user profiles
        [HttpPut]
        [Route("UserProfiles")]
        public ActionResult UserProfiles(UserDataDTO userDataDTO)
        {
            string msg = "";
            try
            {
                var res = _userService.UpdateUser(userDataDTO);  // Call the service to update user profiles
                _logger.LogInformation("Users profiles are listed!!");  // Log successful user profile update
                return Ok(res);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                msg = e.Message;
                _logger.LogError("No User Profile found!!");  // Log error for user profile update
            }
            return BadRequest(msg);  // Return a 400 Bad Request response with the error message
        }
    }
}