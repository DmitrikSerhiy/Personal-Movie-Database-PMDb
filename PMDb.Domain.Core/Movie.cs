using System.Collections.Generic;

namespace PMDb.Domain.Core
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Poster { get; set; }
        public string IMDbId { get; set; }
        public List<MovieGenre> MovieGenre { get; set; }
        public List<MovieDirector> MovieDirector { get; set; }
        public List<MovieWriter> MovieWriter { get; set; }
        public List<MovieActor> MovieActor { get; set; }
        public List<MovieTag> MovieTag { get; set; }
        public Rating Rating { get; set; }


        public Movie()
        {
            MovieGenre = new List<MovieGenre>();
            MovieDirector = new List<Core.MovieDirector>();
            MovieWriter = new List<MovieWriter>();
            MovieActor = new List<MovieActor>();
            MovieTag = new List<MovieTag>();
        }
    }
}
