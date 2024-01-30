namespace BusTicketingWebApplication.Exceptions
{
    public class InvalidBusIdException:Exception
    {
        string msg = "";
        public InvalidBusIdException()
        {
            msg = "The Bus Id is invalid, check again.";
        }
        public override string Message => msg;
    }
}
