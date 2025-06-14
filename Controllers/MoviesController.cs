using MovieProMVC.Data;
using MovieProMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieProMVC.Models.Settings;
using Microsoft.EntityFrameworkCore;
using MovieProMVC.Models.Database;

namespace MovieProMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly IDataMappingService _tmdbMappingService;

        public MoviesController(AppSettings appSettings, ApplicationDbContext context, IImageService imageService, IRemoteMovieService tmdbMovieService, IDataMappingService tmdbMappingService)
        {
            _appSettings = appSettings;
            _context = context;
            _imageService = imageService;
            _tmdbMovieService = tmdbMovieService;
            _tmdbMappingService = tmdbMappingService;
        }

        public async Task<IActionResult> Import()
        {
            var movies = await _context.Movie.ToListAsync();

            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(int id)
        {
            // Warn user if movie is already imported
            if (_context.Movie.Any(m => m.MovieId == id))
            {
                var localMovie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);

                return RedirectToAction("Details", "Movies", new { id = localMovie.Id, local = true }); // From db
            }

            // Import movie from TMDB
            var movieDetails = await _tmdbMovieService.GetMovieDetailsAsync(id);

            // Map the movie details
            var movie = await _tmdbMappingService.MapMovieDetailsAsync(movieDetails);

            // Add the movie to the database
            _context.Add(movie);
            await _context.SaveChangesAsync();

            // Assign it to the default All collection
            await AddToMovieCollection(movie.Id, _appSettings.MovieProSettings.DefaultCollection.Name);

            return RedirectToAction("Import");
        }

        private async Task AddToMovieCollection(int movieId, string collectionName)
        {
            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Name == collectionName);

            _context.Add(new MovieCollection
            {
                MovieId = movieId,
                CollectionId = collection.Id
            });

            await _context.SaveChangesAsync();
        }
    }
}