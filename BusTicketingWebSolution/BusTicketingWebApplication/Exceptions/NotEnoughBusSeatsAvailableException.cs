namespace BusTicketingWebApplication.Exceptions
{
    public class NotEnoughBusSeatsAvailableException:Exception
    {
        string msg = "";
        public NotEnoughBusSeatsAvailableException()
        {
            msg = "No Bookings available yet.";
        }
        public override string Message => msg;
    }
}
