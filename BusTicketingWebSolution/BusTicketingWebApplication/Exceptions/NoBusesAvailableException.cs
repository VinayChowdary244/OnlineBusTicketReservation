using System.Runtime.Serialization;

namespace BusTicketingWebApplication.Exceptions
{
    public class NoBusesAvailableException : Exception
    {
        string msg = "";
        public NoBusesAvailableException()
        {
            msg = "No Buses Available for Booking";
        }
        public override string Message => msg;
    }
}