using Microsoft.AspNetCore.Mvc;

namespace FirelloProject.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chat()
        {
            return View();
        }
    }
}
