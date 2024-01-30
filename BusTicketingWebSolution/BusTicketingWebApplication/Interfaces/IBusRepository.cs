using BusModelLibrary;

namespace BusTicketingWebApplication.Interfaces
{
    public interface IBusRepository
    {
        public Bus Add(Bus item);
        public Bus Delete(int key);
        public Bus GetById(int key);
        public IList<Bus> GetAll();
        public Bus Update(Bus item);
    }
}
