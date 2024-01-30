using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BusTicketingWebApplication.Controllers
{
    // UserController class for handling user-related actions
    public class UserController : Controller
    {
        private readonly IUserService _userService;  // Interface for user-related services

        // Constructor to inject dependencies into the controller
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET action for displaying the registration view
        public IActionResult Register()
        {
            return View();
        }

        // POST action for user registration
        [HttpPost]
        public IActionResult Register(UserViewModel viewModel)
        {
            try
            {
                var user = _userService.Register(viewModel);  // Call the service to register a new user
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");  // Redirect to the home page on successful registration
                }
            }
            catch (DbUpdateException exp)
            {
                ViewBag.Message = "User name already exists";  // Set a message for duplicate username
            }
            catch (Exception)
            {
                ViewBag.Message = "Invalid data. Could not register";  // Set a message for general registration error
                throw;
            }

            return View();  // Return to the registration view with error messages, if any
        }

        // GET action for displaying the login view
        public IActionResult Login()
        {
            return View();
        }

        // POST action for user login
        [HttpPost]
        public IActionResult Login(UserDTO userDTO)
        {
            var result = _userService.Login(userDTO);  // Call the service to perform user login
            if (result != null)
            {
                TempData.Add("username", userDTO.UserName);  // Store the username in TempData for use in other requests
                return RedirectToAction("Index", "Home");  // Redirect to the home page on successful login
            }

            ViewData["Message"] = "Invalid username or password";  // Set a message for unsuccessful login
            return View();  // Return to the login view with an error message
        }
    }
}
