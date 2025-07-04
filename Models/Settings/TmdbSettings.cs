﻿namespace MovieProMVC.Models.Settings
{
    public class TmdbSettings
    {
        public string BaseUrl { get; set; }
        public string BaseImagePath { get; set; }
        public string BaseYouTubePath { get; set; }   
        public QueryOptions QueryOptions { get; set; }
    }

    public class QueryOptions
    {
        public string Language { get; set; }
        public string AppendToResponse { get; set; }
        public string Page { get; set; }
    }
}
