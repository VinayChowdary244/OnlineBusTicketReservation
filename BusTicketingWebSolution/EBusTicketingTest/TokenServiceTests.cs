using System;
using System.Collections.Generic;
using System.Linq;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Reposittories;
using BusTicketingWebApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace EBusTicketingTest
{
    [TestFixture]
    public class TokenServiceTests
    {
        private TokenService _tokenService;
        private IUserRepository _userRepository;
        private TicketingContext _context;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database context
            var options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new TicketingContext(options);
            _userRepository = new UserRepository(_context);
            var configuration = new ConfigurationBuilder().Build(); // No configuration needed for this test
            _tokenService = new TokenService(configuration);

            // Initialize in-memory data for testing
            var user = new User { UserName = "TestUser", Role = "Admin" };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        [Test]
        public void Test_GetToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var userDTO = new UserDTO { UserName = "TestUser", Role = "Admin" };

            // Act
            var result = _tokenService.GetToken(userDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void Test_GetToken_InvalidUser_ReturnsNullToken()
        {
            // Arrange
            var userDTO = new UserDTO { UserName = "InvalidUser", Role = "User" };

            // Act
            var result = _tokenService.GetToken(userDTO);

            // Assert
            Assert.IsNull(result);
        }

        // Add more test cases as needed based on other methods in your TokenService class
    }
}
