using MovieProMVC.Enums;
using MovieProMVC.Interfaces;
using MovieProMVC.Models.Tmdb;

namespace MovieProMVC.Services
{
    public class TmdbMovieService : IRemoteMovieService
    {
        public Task<ActorDetails> GetActorDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetails> GetMovieDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count)
        {
            throw new NotImplementedException();
        }
    }
}