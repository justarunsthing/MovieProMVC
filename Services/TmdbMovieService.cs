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

        public async Task<ActorDetails> GetActorDetailsAsync(int id)
        {
            var actorDetails = new ActorDetails();
            var query = $"{_appSettings.TmdbSettings.BaseUrl}/person/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmdbApiKey },
                { "language", _appSettings.TmdbSettings.QueryOptions.Language }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(ActorDetails));
                actorDetails = (ActorDetails)dcjs.ReadObject(responseStream);
            }

            return actorDetails;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int id)
        {
            var movieDetails = new MovieDetails();
            var query = $"{_appSettings.TmdbSettings.BaseUrl}/movie/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmdbApiKey },
                { "language", _appSettings.TmdbSettings.QueryOptions.Language },
                { "append_to_response", _appSettings.TmdbSettings.QueryOptions.AppendToResponse }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(MovieDetails));
                movieDetails = dcjs.ReadObject(responseStream) as MovieDetails;
            }

            return movieDetails;
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
                movieSearch.Results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TmdbSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{r.poster_path}");
            }

            return movieSearch;
        }
    }
}