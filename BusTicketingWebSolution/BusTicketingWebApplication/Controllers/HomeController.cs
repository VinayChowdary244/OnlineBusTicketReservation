using Microsoft.AspNetCore.Mvc;

namespace BusTicketingWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
