using MovieProMVC.Models.Database;
using MovieProMVC.Models.Tmdb;

namespace MovieProMVC.Models.ViewModels
{
    public class LandingPageViewModel
    {
        public List<Collection> CustomCollections { get; set; }
        public MovieSearch NowPlaying { get; set; }
        public MovieSearch Popular { get; set; }
        public MovieSearch TopRated { get; set; }
        public MovieSearch Upcoming { get; set; }
    }
}