using MovieProMVC.Enums;
using MovieProMVC.Models.Tmdb;

namespace MovieProMVC.Interfaces
{
    public interface IRemoteMovieService
    {
        Task<MovieDetails> GetMovieDetailsAsync(int id);
        Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count);
        Task<ActorDetails> GetActorDetailsAsync(int id);
    }
}