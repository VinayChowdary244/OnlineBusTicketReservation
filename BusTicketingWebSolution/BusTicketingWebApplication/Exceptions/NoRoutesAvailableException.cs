using System.Runtime.Serialization;

namespace BusTicketingWebApplication.Exceptions
{
    public class NoRoutesAvailableException : Exception
    {
        string msg = "";
        public NoRoutesAvailableException()
        {
            msg = "No Routes availsble";
        }
        public override string Message => msg;
    }
}
