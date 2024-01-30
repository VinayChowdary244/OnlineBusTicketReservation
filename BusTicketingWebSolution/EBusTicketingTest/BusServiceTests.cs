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
    public class BusServiceTests
    {
        private BusService _busService;
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
            _busRepository = new BusRepository(_context);
            _busService = new BusService(_busRepository);

            // Initialize in-memory data for testing
            var bus = new Bus { Id = 1, Type = "TestBus", Cost = 10.0f, Start = "StartLocation", End = "EndLocation" };
            _context.Buses.Add(bus);
            _context.SaveChanges();
        }

        [Test]
        public void Test_Add_ValidBus_ReturnsBus()
        {
            // Arrange
            var bus = new Bus { Type = "NewBus", Cost = 15.0f, Start = "StartLocation", End = "EndLocation" };

            // Act
            var result = _busService.Add(bus);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NewBus", result.Type);
        }


       }
}
