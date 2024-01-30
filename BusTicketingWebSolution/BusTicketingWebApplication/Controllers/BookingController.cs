using BusModelLibrary;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// Controller for handling bus ticket booking-related API requests
[Route("api/[controller]")]
[ApiController]
[EnableCors("reactApp")]
public class BookingController : Controller
{
    // Services and logger dependencies
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;
    private readonly IBookedSeatService _bookedSeatService;

    // Constructor to initialize services and logger
    public BookingController(IBookingService bookingService, ILogger<BookingController> logger, IBookedSeatService bookedSeatService)
    {
        _bookingService = bookingService;
        _logger = logger;
        _bookedSeatService = bookedSeatService;
    }

    // API endpoint to create a new booking
    [HttpPost]
    public ActionResult Create(BookingDTO bookingDTO)
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookingService.Add(bookingDTO);
            _logger.LogInformation("Bookings are Added!!");
            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;

            _logger.LogError("Booking is not Added!!");
        }
        return BadRequest(errorMessage);
    }
    // API endpoint to get all bookings (requires admin authorization)
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public ActionResult GetAllBookings()
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookingService.GetBookings();
            _logger.LogInformation("Bookings listed");
            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;

            _logger.LogError("No Such Bookings are present in the collection or in the table");
        }
        return BadRequest(errorMessage);
    }

    // API endpoint to delete a booking (requires admin authorization)
   //[Authorize]
    [Route("Cancel/DeleteBooking")]
    [HttpDelete]
    public ActionResult DeleteBooking(BookingIdDTO bookingIdDTO)
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookingService.RemoveBooking(bookingIdDTO);
            _logger.LogInformation("Bookings are Deleted!!");
            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;

            _logger.LogError("Booking cannot be Deleted!!");
        }
        return BadRequest(errorMessage);
    }
    [Route("CancelledBookings")]
    [HttpPost]
    public ActionResult CancelledBookings(UserNameDTO userNameDTO)
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookingService.CancelledBookingsList(userNameDTO);
            _logger.LogInformation("Cancelled Bookings are listed!!");
            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;

            _logger.LogError("Cancelled bookings cannot be listed!!");
        }
        return BadRequest(errorMessage);

    }

    // API endpoint to get a list of booked seats for a specific bus
    [HttpPost]
    [Route("BookedSeatsList")]
    public ActionResult BookedSeatsList(BusIdDTO busIdDTO)
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookedSeatService.GetSeatsById(busIdDTO);
            _logger.LogInformation("Booking done");

            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            _logger.LogError("Booking not done");
        }
        return BadRequest(errorMessage);
    }

    // API endpoint to get booked seats for a specific bus and date
    [Route("BookedSeats")]
    [HttpPost]
    public ActionResult BookedSeats(BookedSeatsDTO bookedSeatsDTO)
    {
        string errorMessage = string.Empty;
        try
        {
            var result = _bookingService.BookedSeatsInTheBus(bookedSeatsDTO);
            _logger.LogInformation("Booked Seats in the bus fetched");

            return Ok(result);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            _logger.LogError("Booked Seats not fetched");
        }
        return BadRequest(errorMessage);
    }
}
