using BusModelLibrary;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using Microsoft.EntityFrameworkCore;
namespace BusTicketingWebApplication.Repositories
{
    // BusRouteRepository class responsible for database operations related to bus routes
    public class BusRouteRepository : IBusRouteRepository
    {
        // DbContext for interacting with the database
        private readonly TicketingContext _context;

        // Constructor to initialize the repository with the DbContext
        public BusRouteRepository(TicketingContext context)
        {
            _context = context;
        }

        // Method to add a new bus route to the database
        public BusRoute Add(BusRoute item)
        {
            _context.BusRoutes.Add(item);
            _context.SaveChanges();
            return item;
        }

        // Method to delete a bus route from the database based on its key
        public BusRoute Delete(int key)
        {
            // Retrieve bus route by key
            var busRoute = GetById(key);

            // Check if the bus route exists
            if (busRoute != null)
            {
                // Remove the bus route from the database
                _context.BusRoutes.Remove(busRoute);
                _context.SaveChanges();
                return busRoute;
            }

            return null;  // Return null if bus route not found
        }

        // Method to retrieve a list of all bus routes from the database
        public IList<BusRoute> GetAll()
        {
            // Check if there are no bus routes in the database
            if (_context.BusRoutes.Count() == 0)
            {
                return null;  // Return null if no bus routes found
            }

            return _context.BusRoutes.ToList();  // Return a list of all bus routes
        }

        // Method to retrieve a bus route from the database based on its key
        public BusRoute GetById(int key)
        {
            // Retrieve bus route using LINQ based on the key
            var cus = _context.BusRoutes.SingleOrDefault(x => x.RouteId == key);
            return cus;
        }

        // Method to update a bus route in the database
        public BusRoute Update(BusRoute entity)
        {
            // Retrieve existing bus route by its ID
            var cus = GetById(entity.RouteId);

            // Check if the bus route exists
            if (cus != null)
            {
                // Mark the bus route as modified and save changes to the database
                _context.Entry<BusRoute>(cus).State = EntityState.Modified;
                _context.SaveChanges();
                return cus;
            }

            return null;  // Return null if bus route not found
        }
    }
}
