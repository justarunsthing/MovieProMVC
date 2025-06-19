using Microsoft.AspNetCore.Mvc;

namespace MovieProMVC.Controllers
{
    public class ActorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
