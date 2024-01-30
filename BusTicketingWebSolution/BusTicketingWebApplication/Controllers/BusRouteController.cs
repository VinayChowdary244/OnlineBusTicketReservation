using BusModelLibrary;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingWebApplication.Controllers
{
    // BusRouteController class for handling bus route-related HTTP requests
    [Route("api/[controller]")]  // Route prefix for the controller
    [ApiController]  // Indicates that the controller responds to HTTP API requests
    [EnableCors("reactApp")]  // Enabling CORS for the specified policy ("reactApp")
    public class BusRouteController : ControllerBase
    {
        private readonly IBusRouteService _busRouteService;  // Interface for bus route-related services
        private readonly ILogger<BusRouteController> _logger;  // Logger for logging information and errors

        // Constructor to inject dependencies into the controller
        public BusRouteController(IBusRouteService busRouteService, ILogger<BusRouteController> logger)
        {
            _busRouteService = busRouteService;
            _logger = logger;
        }

        // GET method to retrieve all bus routes
        [Authorize]  // Authorization attribute specifying that any authenticated user can access
        [HttpGet]
        public ActionResult Get()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busRouteService.GetRoutes();  // Call the service to get a list of bus routes
                _logger.LogInformation("Bus Routes listed");  // Log successful listing
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("No Such Bus Routes are present in the collection");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // POST method to create a new bus route
        [Authorize(Roles = "Admin")]  // Authorization attribute specifying only users with the "Admin" role can access
        [HttpPost]
        public ActionResult Create(BusRoute busRoute)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busRouteService.Add(busRoute);  // Call the service to add a new bus route
                _logger.LogInformation("Bus Routes are Added!!");  // Log successful addition
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Bus Route is not added!!");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }
    }
}
