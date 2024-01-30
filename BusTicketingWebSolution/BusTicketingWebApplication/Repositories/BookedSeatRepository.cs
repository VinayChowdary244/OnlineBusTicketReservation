using BusModelLibrary;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BusTicketingWebApplication.Repositories
{
    /// <summary>
    /// Repository class for managing booked seat operations.
    /// </summary>
    public class BookedSeatRepository : IBookedSeatRepository
    {
        private readonly TicketingContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookedSeatRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BookedSeatRepository(TicketingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new booked seat.
        /// </summary>
        /// <param name="item">The booked seat object to be added.</param>
        /// <returns>The added booked seat.</returns>
        public BookedSeat Add(BookedSeat item)
        {
            _context.BookedSeats.Add(item);
            _context.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes a booked seat by its ID.
        /// </summary>
        /// <param name="key">The ID of the booked seat to be deleted.</param>
        /// <returns>The deleted booked seat.</returns>
        public BookedSeat Delete(int key)
        {
            var seat = GetById(key);
            if (seat != null)
            {
                _context.BookedSeats.Remove(seat);
                _context.SaveChanges();
                return seat;
            }
            return null;
        }

        /// <summary>
        /// Gets a booked seat by its ID.
        /// </summary>
        /// <param name="key">The ID of the booked seat to retrieve.</param>
        /// <returns>The booked seat with the specified ID.</returns>
        public BookedSeat GetById(int key)
        {
            var seat = _context.BookedSeats.SingleOrDefault(x => x.Id == key);
            return seat;
        }

        /// <summary>
        /// Gets a list of all booked seats.
        /// </summary>
        /// <returns>The list of booked seats.</returns>
        public IList<BookedSeat> GetAll()
        {
            if (_context.BookedSeats.Count() == 0)
            {
                return null;
            }
            return _context.BookedSeats.ToList();
        }

        /// <summary>
        /// Updates a booked seat entity.
        /// </summary>
        /// <param name="entity">The booked seat entity to be updated.</param>
        /// <returns>The updated booked seat.</returns>
        public BookedSeat Update(BookedSeat entity)
        {
            var seat = GetById(entity.Id);
            if (seat != null)
            {
                _context.Entry<BookedSeat>(seat).State = EntityState.Modified;
                _context.SaveChanges();
                return seat;
            }
            return null;
        }
    }
}
