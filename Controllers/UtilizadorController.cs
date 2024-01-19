using Microsoft.AspNetCore.Mvc;

namespace Music4Every1.Controllers
{
    public class UtilizadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Welcome()
        {
            return "This is the welcome action method...";
        }
    }
}
