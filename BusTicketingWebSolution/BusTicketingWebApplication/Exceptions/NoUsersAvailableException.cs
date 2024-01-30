namespace BusTicketingWebApplication.Exceptions
{
    public class NoUsersAvailableException: Exception
    {
        string msg = "";
        public NoUsersAvailableException()
        {
            msg = "No users available";
        }
        public override string Message => msg;
    }
}
