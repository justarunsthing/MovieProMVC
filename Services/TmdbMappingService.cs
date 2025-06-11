using MovieProMVC.Interfaces;
using MovieProMVC.Models.Tmdb;
using MovieProMVC.Models.Database;
using MovieProMVC.Models.Settings;
using Microsoft.Extensions.Options;
using MovieProMVC.Enums;

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

        public async Task<Movie> MapMovieDetailsAsync(MovieDetails movie)
        {
            Movie newMovie = null;

            try
            {
                newMovie = new Movie
                {
                    MovieId = movie.id,
                    Title = movie.title,
                    TagLine = movie.tagline,
                    Overview = movie.overview,
                    RunTime = movie.runtime,
                    VoteAverage = movie.vote_average,
                    ReleaseDate = DateTime.Parse(movie.release_date),
                    TrailerUrl = BuildTrailerPath(movie.videos),
                    Backdrop = await EncodeBackdropImageAsync(movie.backdrop_path),
                    BackdropType = BuildImageType(movie.backdrop_path),
                    Poster = await EncodePosterImageAsync(movie.poster_path),
                    PosterType = BuildImageType(movie.poster_path),
                    Rating = GetRating(movie.release_dates)
                };

                var castMembers = movie.credits.cast.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.cast_id)
                                                    .Select(g => g.FirstOrDefault())
                                                    .Take(20)
                                                    .ToList();

                castMembers.ForEach(member =>
                {
                    newMovie.Cast.Add(new MovieCast
                    {
                        CastId = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Character = member.character,
                        ImageUrl = BuildCastImage(member.profile_path)
                    });
                });

                castMembers.ForEach(member =>
                {
                    newMovie.Crew.Add(new MovieCrew
                    {
                        CrewId = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Job = member.job,
                        ImageUrl = BuildCastImage(member.profile_path)
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MapMovieDetailsAsync: {ex.Message}");
            }

            return newMovie;
        }

        private string BuildTrailerPath(Videos videos)
        {
            var videoKey = videos.results.FirstOrDefault(r => r.type.ToLower().Trim() == "trailer" && r.key != "")?.key;

            return string.IsNullOrEmpty(videoKey)
                ? videoKey
                : $"{_appSettings.TmdbSettings.BaseYouTubePath}{videoKey}";
        }

        private async Task<byte[]> EncodeBackdropImageAsync(string path)
        {
            var backdropPath = $"{_appSettings.TmdbSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultBackdropSize}/{path}";

            return await _imageService.EncodeImageUrlAsync(backdropPath);
        }

        private string BuildImageType(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            return $"image/{Path.GetExtension(path).TrimStart('.')}";
        }

        private async Task<byte[]> EncodePosterImageAsync(string path)
        {
            var posterPath = $"{_appSettings.TmdbSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{path}";
            return await _imageService.EncodeImageUrlAsync(posterPath);
        }

        private MovieRating GetRating(Release_Dates dates)
        {
            var movieRating = MovieRating.NR;
            var certification = dates.results.FirstOrDefault(r => r.iso_3166_1 == "US");

            if (certification != null)
            {
                var apiRating = certification.release_dates.FirstOrDefault(c => c.certification != "")?.certification.Replace(".", "");

                if (!string.IsNullOrEmpty(apiRating))
                {
                    movieRating = (MovieRating)Enum.Parse(typeof(MovieRating), apiRating, true);
                }
            }

            return movieRating;
        }

        private string BuildCastImage(string profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
                return _appSettings.MovieProSettings.DefaultCastImage;

            return $"{_appSettings.TmdbSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{profilePath}";
        }
    }
}