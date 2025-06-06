using MovieProMVC.Models.Database;
using MovieProMVC.Models.Tmdb;

namespace MovieProMVC.Interfaces
{
    public interface IDataMappingService
    {
        Task<Movie> MapMovieDetailsAsync(MovieDetails movie);
        ActorDetails MapActorDetails(ActorDetails actor);
    }
}