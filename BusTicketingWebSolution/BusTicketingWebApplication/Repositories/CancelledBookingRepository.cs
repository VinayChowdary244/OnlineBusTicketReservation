using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using Microsoft.EntityFrameworkCore;
namespace BusTicketingWebApplication.Repositories
{
    // CancelledBookingRepository class responsible for database operations related to cancelled bookings
    public class CancelledBookingRepository : ICancelledBookingRepository
    {
        // DbContext for interacting with the database
        private readonly TicketingContext _context;

        // Constructor to initialize the repository with the DbContext
        public CancelledBookingRepository(TicketingContext context)
        {
            _context = context;
        }

        // Method to add a new cancelled booking to the database
        public CancelledBooking Add(CancelledBooking item)
        {
            _context.CancelledBookings.Add(item);
            _context.SaveChanges();
            return item;
        }

        // Method to delete a cancelled booking from the database based on its key
        public CancelledBooking Delete(int key)
        {
            // Retrieve cancelled booking by key
            var bus = GetById(key);

            // Check if the cancelled booking exists
            if (bus != null)
            {
                // Remove the cancelled booking from the database
                _context.CancelledBookings.Remove(bus);
                _context.SaveChanges();
                return bus;
            }

            return null;  // Return null if cancelled booking not found
        }

        // Method to retrieve a cancelled booking from the database based on its key
        public CancelledBooking GetById(int key)
        {
            // Retrieve cancelled booking using LINQ based on the key
            var cus = _context.CancelledBookings.SingleOrDefault(x => x.BookingId == key);
            return cus;
        }

        // Method to retrieve a list of all cancelled bookings from the database
        public IList<CancelledBooking> GetAll()
        {
            // Check if there are no cancelled bookings in the database
            if (_context.CancelledBookings.Count() == 0)
            {
                return null;  // Return null if no cancelled bookings found
            }

            return _context.CancelledBookings.ToList();  // Return a list of all cancelled bookings
        }

        // Method to update a cancelled booking in the database
        public CancelledBooking Update(CancelledBooking entity)
        {
            // Retrieve existing cancelled booking by its ID
            var cus = GetById(entity.BusId);

            // Check if the cancelled booking exists
            if (cus != null)
            {
                // Mark the cancelled booking as modified and save changes to the database
                _context.Entry<CancelledBooking>(cus).State = EntityState.Modified;
                _context.SaveChanges();
                return cus;
            }

            return null;  // Return null if cancelled booking not found
        }
    }
}

