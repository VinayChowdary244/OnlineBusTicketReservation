using BusModelLibrary;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Repositories;
using BusTicketingWebApplication.Models;
using BusTicketingWebApplication.Exceptions;

namespace BusTicketingWebApplication.Services
{
    public class BusRouteService : IBusRouteService
    {
        private readonly IBusRouteRepository _routeRepository;

        public BusRouteService(IBusRouteRepository repository)
        {
            _routeRepository = repository;
        }
        public BusRoute Add(BusRoute busroute)
        {
            var result = _routeRepository.Add(busroute);
            return result;
            
        }

        public List<BusRoute> GetRoutes()
        {
            var busroutes = _routeRepository.GetAll();
            if (busroutes != null)
            {
                return busroutes.ToList();
            }
            throw new NoRoutesAvailableException();
        }
    }
}
