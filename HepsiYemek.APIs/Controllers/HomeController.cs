using Microsoft.AspNetCore.Mvc;

namespace HepsiYemek.APIs.Controllers
{
    public class HomeController  : ControllerBase
    {
        public IActionResult Index()
        {

            return Content("siri");
        }
    }
}