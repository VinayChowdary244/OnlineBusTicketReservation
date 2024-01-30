namespace BusTicketingWebApplication.Exceptions
{
    public class NoSuchRoutesAvailableException : Exception
    {
        string msg = "";
        public NoSuchRoutesAvailableException()
        {
            msg = "No routes found that are related to your search";
        }
        public override string Message => msg;
    }
}
