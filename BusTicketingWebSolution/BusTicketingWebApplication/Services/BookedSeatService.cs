using BusModelLibrary;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Repositories;

namespace BusTicketingWebApplication.Services
{
    /// <summary>
    /// Service class for managing booked seat-related operations.
    /// </summary>
    public class BookedSeatService : IBookedSeatService
    {
        private readonly IBookedSeatRepository _bookedSeatRepository;
        private readonly IBusRepository _busRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookedSeatService"/> class.
        /// </summary>
        /// <param name="bookedSeatRepository">The repository for booked seats.</param>
        /// <param name="busRepository">The repository for buses.</param>
        public BookedSeatService(IBookedSeatRepository bookedSeatRepository, IBusRepository busRepository)
        {
            _bookedSeatRepository = bookedSeatRepository;
            _busRepository = busRepository;
        }

        /// <summary>
        /// Gets the list of booked seats for a specific bus.
        /// </summary>
        /// <param name="busIdDTO">The DTO containing the bus ID.</param>
        /// <returns>The list of booked seat IDs for the specified bus.</returns>
        public List<int> GetSeatsById(BusIdDTO busIdDTO)
        {
            var bus = _bookedSeatRepository.GetById(busIdDTO.Id);
            if (bus != null)
            {
                return bus.BookedSeats;
            }
            return null;
        }
    }
}
