namespace BusTicketingWebApplication.Exceptions
{
    public class InvalidNoOfTicketsEnteredException :Exception
    {
        string msg = "";
        public InvalidNoOfTicketsEnteredException()
        {
            msg = "Range of Tickets Booking should be between 0 and 40";
        }
        public override string Message => msg;
    }
}
