using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusTicketingWebApplication.Exceptions;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusModelLibrary;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Services;
using Microsoft.AspNetCore.Cors;
namespace BusTicketingWebApplication.Controllers
{
    // BusController class for handling bus-related HTTP requests
    [Route("api/[controller]")]  // Route prefix for the controller
    [ApiController]  // Indicates that the controller responds to HTTP API requests
    [EnableCors("reactApp")]  // Enabling CORS for the specified policy ("reactApp")
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;  // Interface for bus-related services
        private readonly ILogger<BusController> _logger;  // Logger for logging information and errors

        // Constructor to inject dependencies into the controller
        public BusController(IBusService busService, ILogger<BusController> logger)
        {
            _busService = busService;
            _logger = logger;
        }

        // GET method to retrieve all buses
        [Authorize(Roles = "Admin")]  // Authorization attribute specifying roles allowed to access
        [HttpGet]
        public ActionResult GetAllBusses()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busService.GetBuses();  // Call the service to get a list of buses
                _logger.LogInformation("Buses listed");  // Log successful listing
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("No Such Buses are present in the collection or in the table");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // POST method to create a new bus
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Bus bus)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busService.Add(bus);  // Call the service to add a new bus
                _logger.LogInformation("Buses are Added!!");  // Log successful addition
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Bus is not Added!!");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // DELETE method to remove a bus by its ID
        [Authorize(Roles = "Admin")]
        [Route("DeleteBus")]
        [HttpDelete]
        public ActionResult DeleteBus(BusIdDTO busIdDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busService.RemoveBus(busIdDTO);  // Call the service to remove a bus
                _logger.LogInformation("Bus is Deleted!!");  // Log successful deletion
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Bus is not Deleted!!");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        // PUT method to update information for a specific bus
        [Authorize(Roles = "Admin")]
        [Route("UpdateBus")]
        [HttpPut]
        public ActionResult UpdateBus(BusDTO busDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busService.UpdateBus(busDTO);  // Call the service to update bus information
                _logger.LogInformation("Bus is Updated!!");  // Log successful update
                return Ok(result);  // Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Bus is not Updated!!");  // Log error
            }
            return BadRequest(errorMessage);  // Return a 400 Bad Request response with the error message
        }

        [HttpPost]
        [Route("GetBusById")]
        public ActionResult GetBusById(BusIdDTO busIdDTO)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _busService.GetBusById(busIdDTO); // Call the service to update bus information
                _logger.LogInformation("Busses listed");// Log successful update

                return Ok(result);// Return a 200 OK response with the result
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                _logger.LogError("Error Occured,Product not listed");   // Log error
            }
            return BadRequest(errorMessage);// Return a 400 Bad Request response with the error message
        }

    }

}