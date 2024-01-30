// Import necessary namespaces and dependencies
using BusModelLibrary;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using Microsoft.EntityFrameworkCore;

// Repository class for managing bus data in the database
namespace BusTicketingWebApplication.Repositories
{
    public class BusRepository : IBusRepository
    {
        // Database context for bus entities
        private readonly TicketingContext _context;

        // Constructor to initialize the database context
        public BusRepository(TicketingContext context)
        {
            _context = context;
        }

        // Method to add a new bus entity to the database
        public Bus Add(Bus item)
        {
            _context.Buses.Add(item);
            _context.SaveChanges();
            return item;
        }

        // Method to delete a bus entity from the database by ID
        public Bus Delete(int key)
        {
            var bus = GetById(key);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
                _context.SaveChanges();
                return bus;
            }
            return null;
        }

        // Method to retrieve a bus entity from the database by ID
        public Bus GetById(int key)
        {
            var bus = _context.Buses.SingleOrDefault(x => x.Id == key);
            return bus;
        }

        // Method to retrieve a list of all bus entities from the database
        public IList<Bus> GetAll()
        {
            if (_context.Buses.Count() == 0)
            {
                return null;
            }
            return _context.Buses.ToList();
        }

        // Method to update a bus entity in the database
        public Bus Update(Bus entity)
        {
            var bus = GetById(entity.Id);
            if (bus != null)
            {
                _context.Entry<Bus>(bus).State = EntityState.Modified;
                _context.SaveChanges();
                return bus;
            }
            return null;
        }

        public void SetSampleBus(Bus sampleBus)
        {
            throw new NotImplementedException();
        }
    }
}
