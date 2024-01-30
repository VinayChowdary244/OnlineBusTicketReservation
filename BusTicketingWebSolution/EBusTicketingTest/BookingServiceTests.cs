using System;
using System.Collections.Generic;
using System.Linq;
using BusModelLibrary;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Exceptions;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Repositories;
using BusTicketingWebApplication.Reposittories;
using BusTicketingWebApplication.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EBusTicketingTest
{
    [TestFixture]
    public class BookingServiceTests
    {
        private BookingService _bookingService;
        private TicketingContext _context;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database context
            var options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new TicketingContext(options);
            _bookingService = new BookingService(
                new BookingRepository(_context),
                new BusRepository(_context),
                new UserRepository(_context),
                new BookedSeatRepository(_context),
                new CancelledBookingRepository(_context)
            );
        }

      
        [Test]
        public void Test_RemoveBooking_ValidBookingId_ReturnsBookingIdDTO()
        {
            // Arrange
            var bus = 
                new Bus
                {
                    Id = 1,
                    DriverAge = 32,
                    DriverName = "satish",
                    DriverPhone = "987456325",
                    DriverRating = 4,
                    Start = "Eluru",
                    End = "Hyderabad",
                    Type = "A/C",
                    Duration = "6",
                    StartTime = "8:45"
                };
            _context.Buses.Add(bus);
            _context.SaveChanges();

            var booking = new Booking { BookingId = 1, SelectedSeats = {2,3},UserName="Uday"/* Set your sample data */ };
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var bookingIdDTO = new BookingIdDTO
            {
                Id = 1 // Assuming a valid booking ID
            };

            // Act
            var result = _bookingService.RemoveBooking(bookingIdDTO);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions based on your requirements
        }

        [Test]
        public void Test_RemoveBooking_InvalidBookingId_ReturnsNull()
        {
            // Arrange
            var bookingIdDTO = new BookingIdDTO
            {
                Id = 99 // Assuming an invalid booking ID
            };

            // Act
            var result = _bookingService.RemoveBooking(bookingIdDTO);

            // Assert
            Assert.IsNull(result);
        }

        // Add more test cases as needed based on other methods in your BookingService class
    }
}
