using BusModelLibrary;  // Importing the BusModelLibrary namespace for Booking-related classes
using BusTicketingWebApplication.Contexts;  // Importing the DbContext for database interactions
using BusTicketingWebApplication.Interfaces;  // Importing the interface for the Booking repository
using BusTicketingWebApplication.Models;  // Importing models for Booking
using Microsoft.EntityFrameworkCore;  // Importing Entity Framework Core for database operations

namespace BusTicketingWebApplication.Repositories
{
    // BookingRepository class responsible for database operations related to bookings
    public class BookingRepository : IBookingRepository
    {
        // DbContext for interacting with the database
        private readonly TicketingContext _context;

        // Constructor to initialize the repository with the DbContext
        public BookingRepository(TicketingContext context)
        {
            _context = context;
        }

        // Method to add a new booking to the database
        public Booking Add(Booking item)
        {
            _context.Bookings.Add(item);
            _context.SaveChanges();
            return item;
        }

        // Method to delete a booking from the database based on its key
        public Booking Delete(int key)
        {
            // Retrieve booking by key
            var booking = GetById(key);

            // Check if the booking exists
            if (booking != null)
            {
                // Remove the booking from the database
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                return booking;
            }

            return null;  // Return null if booking not found
        }

        // Method to retrieve a booking from the database based on its key
        public Booking GetById(int key)
        {
            // Retrieve booking using LINQ based on the key
            var cus = _context.Bookings.SingleOrDefault(x => x.BookingId == key);
            return cus;
        }

        // Method to retrieve a list of all bookings from the database
        public IList<Booking> GetAll()
        {
            // Check if there are no bookings in the database
            if (_context.Bookings.Count() == 0)
            {
                return null;  // Return null if no bookings found
            }

            return _context.Bookings.ToList();  // Return a list of all bookings
        }

        // Method to update a booking in the database
        public Booking Update(Booking entity)
        {
            // Retrieve existing booking by its ID
            var cus = GetById(entity.BookingId);

            // Check if the booking exists
            if (cus != null)
            {
                // Mark the booking as modified and save changes to the database
                _context.Entry<Booking>(cus).State = EntityState.Modified;
                _context.SaveChanges();
                return cus;
            }

            return null;  // Return null if booking not found
        }

        public void SetSampleBooking(Booking sampleBooking)
        {
            throw new NotImplementedException();
        }
    }
}
