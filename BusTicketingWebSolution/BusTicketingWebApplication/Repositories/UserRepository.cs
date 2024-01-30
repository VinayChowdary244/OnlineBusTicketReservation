using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using Microsoft.EntityFrameworkCore;
namespace BusTicketingWebApplication.Reposittories
{
    // UserRepository class responsible for database operations related to users
    public class UserRepository : IUserRepository
    {
        // DbContext for interacting with the database
        private readonly TicketingContext _context;

        // Constructor to initialize the repository with the DbContext
        public UserRepository(TicketingContext context)
        {
            _context = context;
        }

        // Method to add a new user to the database
        public User Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        // Method to delete a user from the database based on its key (username)
        public User Delete(string key)
        {
            // Retrieve user by key
            var user = GetById(key);

            // Check if the user exists
            if (user != null)
            {
                // Remove the user from the database
                _context.Users.Remove(user);
                _context.SaveChanges();
                return user;
            }

            return null;  // Return null if user not found
        }

        // Method to retrieve a list of all users from the database
        public IList<User> GetAll()
        {
            // Check if there are no users in the database
            if (_context.Users.Count() == 0)
                return null;  // Return null if no users found

            return _context.Users.ToList();  // Return a list of all users
        }

        // Method to retrieve a user from the database based on its key (username)
        public User GetById(string key)
        {
            // Retrieve user using LINQ based on the key
            var user = _context.Users.SingleOrDefault(u => u.UserName == key);
            return user;
        }

        // Method to update a user in the database
        public User Update(User entity)
        {
            // Retrieve existing user by its username
            var user = GetById(entity.UserName);

            // Check if the user exists
            if (user != null)
            {
                // Mark the user as modified and save changes to the database
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
                return user;
            }

            return null;  // Return null if user not found
        }
    }
}