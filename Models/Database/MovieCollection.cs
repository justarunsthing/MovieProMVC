namespace MovieProMVC.Models.Database
{
    public class MovieCollection
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int MovieId { get; set; }
        public int Order { get; set; }

        // Store entire record referenced by the FKs
        public Collection Collection { get; set; }
        public Movie Movie { get; set; }
    }
}