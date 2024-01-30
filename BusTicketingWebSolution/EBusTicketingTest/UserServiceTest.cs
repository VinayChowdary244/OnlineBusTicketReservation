using System.IO;
using System.Text;
using BusTicketingWebApplication.Contexts;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models.DTOs;
using BusTicketingWebApplication.Repositories;
using BusTicketingWebApplication.Reposittories;
using BusTicketingWebApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace EBusTicketingTest
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserRepository repository;
        private IBusRepository busrepository;
        private IBookingRepository bookingRepository;

        [SetUp]
       
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<TicketingContext>()
                                .UseInMemoryDatabase("dbTestCustomer")//a database that gets created temp for testing purpose
                                .Options;
            TicketingContext context = new TicketingContext(dbOptions);
            repository = new UserRepository(context);

        }

        [Test]
        [TestCase("Test", "test123")]
        [TestCase("Test", "test321")]
        public void LoginTest(string un, string pass)
        {

            //Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var tokenService = new TokenService(configurationBuilder.Build());
            IUserService userService = new UserService(repository, tokenService);
            userService.Register(new UserDTO
            {
                UserName = un,
                Password = pass,
                Role = "Admin",
                City="Eluru",
                Email="e@gmail",
                Phone="9685745"
            });
            //Action
            var resulut = userService.Login(new UserDTO { UserName = "Test", Password = "test123", Role = "Admin" });
            //Assert
            Assert.AreEqual("Test", resulut.UserName);
        }
    }
}

