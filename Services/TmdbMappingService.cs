using MovieProMVC.Interfaces;
using MovieProMVC.Models.Tmdb;
using MovieProMVC.Models.Database;
using MovieProMVC.Models.Settings;
using Microsoft.Extensions.Options;

namespace MovieProMVC.Services
{
    public class TmdbMappingService : IDataMappingService
    {
        private readonly AppSettings _appSettings;
        private readonly IImageService _imageService;

        public TmdbMappingService(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }

        public ActorDetails MapActorDetails(ActorDetails actor)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> MapMovieDetailsAsync(MovieDetails movie)
        {
            throw new NotImplementedException();
        }
    }
}
