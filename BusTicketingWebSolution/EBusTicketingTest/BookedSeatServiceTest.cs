using System;
using System.Collections.Generic;
using System.Linq;
using BusModelLibrary;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Repositories;
using BusTicketingWebApplication.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EBusTicketingTest
{
    [TestFixture]
    public class BookedSeatServiceTests
    {
        private BookedSeatService _bookedSeatService;
        private IBookedSeatRepository _bookedSeatRepository;
        private IBusRepository _busRepository;
        private TicketingContext _context;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database context
            var options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new TicketingContext(options);
            _bookedSeatRepository = new BookedSeatRepository(_context);
            _busRepository = new BusRepository(_context);

            _bookedSeatService = new BookedSeatService(_bookedSeatRepository, _busRepository);

            // Initialize in-memory data for testing
            var bus = new Bus { Id = 1, DriverAge=32,DriverName="satish",DriverPhone="987456325",DriverRating=4,Start="Eluru",End="Hyderabad",Type="A/C",Duration="6",StartTime="8:45"
                        
            };
            var bookedSeats = new BookedSeat { BusId = 1, Date="26/12/2023" ,BookedSeats = new List<int> { 1, 2, 3 } };

            _context.Buses.Add(bus);
            _context.BookedSeats.Add(bookedSeats);
            _context.SaveChanges();
        }

        [Test]
        public void Test_GetSeatsById_ValidBusId_ReturnsBookedSeats()
        {
            // Arrange
            var busIdDTO = new BusIdDTO { Id = 1 };

            // Act
            var result = _bookedSeatService.GetSeatsById(busIdDTO);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, result);
        }

        [Test]
        public void Test_GetSeatsById_InvalidBusId_ReturnsNull()
        {
            // Arrange
            var busIdDTO = new BusIdDTO
            {
                Id = 1,

            };

            // Act
            var result = _bookedSeatService.GetSeatsById(busIdDTO);

            // Assert
            Assert.IsNull(result);
        }

        // Add more test cases as needed based on other methods in your BookedSeatService class
    }
}
