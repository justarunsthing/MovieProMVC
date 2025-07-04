﻿namespace MovieProMVC.Models.Tmdb
{

    public class MovieSearch
    {
        public int Page { get; set; }
        public MovieSearchResult[] Results { get; set; } = Array.Empty<MovieSearchResult>();
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    public class MovieSearchResult
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; } = [];
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
