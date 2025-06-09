using MovieProMVC.Enums;
using MovieProMVC.Interfaces;
using MovieProMVC.Models.Tmdb;
using MovieProMVC.Models.Settings;
using Microsoft.Extensions.Options;

namespace MovieProMVC.Services
{
    public class TmdbMovieService : IRemoteMovieService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClient;

        public TmdbMovieService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

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