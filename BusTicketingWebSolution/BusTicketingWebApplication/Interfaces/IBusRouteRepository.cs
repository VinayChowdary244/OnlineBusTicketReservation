using BusTicketingWebApplication.Models;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBusRouteRepository
    {

        public BusRoute Add(BusRoute item);
        public BusRoute Delete(int key);
        public BusRoute GetById(int key);
        public IList<BusRoute> GetAll();
        public BusRoute Update(BusRoute item);
    }
}
