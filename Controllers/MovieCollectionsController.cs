using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieProMVC.Data;
using MovieProMVC.Models.Database;

namespace MovieProMVC.Controllers
{
    public class MovieCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieCollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            id ??= (await _context.Collection.FirstOrDefaultAsync(c => c.Name.ToUpper() == "ALL")).Id;

            var allMovieIds = await _context.Movie.Select(m => m.Id).ToListAsync();
            var movieIdsInCollection = await _context.MovieCollection
                                            .Where(m => m.CollectionId == id)
                                            .OrderBy(m => m.Order)
                                            .Select(m => m.MovieId)
                                            .ToListAsync();
            var movieIdsNotInCollection = allMovieIds.Except(movieIdsInCollection);
            var moviesInCollection = new List<Movie>();
            movieIdsInCollection.ForEach(movieId => moviesInCollection.Add(_context.Movie.Find(movieId)));

            var moviesNotInCollection = await _context.Movie.AsNoTracking().Where(m => movieIdsNotInCollection.Contains(m.Id)).ToListAsync();

            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", id);
            ViewData["IdsInCollection"] = new MultiSelectList(moviesInCollection, "Id", "Title");
            ViewData["IdsNotInCollection"] = new MultiSelectList(moviesNotInCollection, "Id", "Title");

            return View();
        }
    }
}