using MovieProMVC.Enums;
using MovieProMVC.Interfaces;
using MovieProMVC.Models.Tmdb;
using MovieProMVC.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.Serialization.Json;

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

        public async Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count)
        {
            var movieSearch = new MovieSearch();
            var query = $"{_appSettings.TmdbSettings.BaseUrl}/movie/{category}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmdbApiKey },
                { "language", _appSettings.TmdbSettings.QueryOptions.Language },
                { "page", _appSettings.TmdbSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));

                using var responseStream = await response.Content.ReadAsStreamAsync();
                movieSearch = (MovieSearch)dcjs.ReadObject(responseStream);
                movieSearch.Results = movieSearch.Results.Take(count).ToArray();
                movieSearch.Results.ToList().ForEach(r => r.PosterPath = $"{_appSettings.TmdbSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{r.PosterPath}");
            }

            return movieSearch;
        }
    }
}