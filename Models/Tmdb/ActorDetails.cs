﻿namespace MovieProMVC.Models.Tmdb
{
    public class ActorDetails
    {
        public bool adult { get; set; }
        public string[] also_known_as { get; set; } = [];
        public string biography { get; set; }
        public string birthday { get; set; }
        public object deathday { get; set; }
        public int gender { get; set; }
        public object homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string place_of_birth { get; set; }
        public float popularity { get; set; }
        public string profile_path { get; set; }
    }
}