using Microsoft.VisualBasic;

namespace BusTicketingWebApplication.Exceptions
{
    public class NoCancelledBookingsException :Exception
    {
        string msg = "";
        public NoCancelledBookingsException() { 
        msg="No Cancellings Available!!";
        }
        public override string Message => msg;
    }
}
