using Microsoft.AspNetCore.Mvc;
using MovieProMVC.Interfaces;

namespace MovieProMVC.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly IDataMappingService _mappingService;

        public ActorsController(IRemoteMovieService tmdbMovieService, IDataMappingService mappingService)
        {
            _tmdbMovieService = tmdbMovieService;
            _mappingService = mappingService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
