using Microsoft.AspNetCore.Mvc;

namespace MovieProMVC.Controllers
{
    public class MovieCollectionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
