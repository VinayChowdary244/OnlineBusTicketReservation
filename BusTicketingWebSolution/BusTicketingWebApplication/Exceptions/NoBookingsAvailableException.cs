namespace BusTicketingWebApplication.Exceptions
{
    public class NoBookingsAvailableException : Exception
    {
        string msg = "";
        public NoBookingsAvailableException()
        {
            msg = "No Bookings available yet.";
        }
        public override string Message => msg;
    }
}
