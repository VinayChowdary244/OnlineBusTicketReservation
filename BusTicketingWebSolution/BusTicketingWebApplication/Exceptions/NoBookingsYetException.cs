namespace BusTicketingWebApplication.Exceptions
{
    public class NoBookingsYetException:Exception
    {
        string msg = "";
        public NoBookingsYetException()
        {
            msg = "No Bookings done Yet!!";
        }
        public override string Message => msg;
    }
}
