using System.Collections.Generic;

namespace PMDb.Domain.Core
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieGenre> MovieGenre { get; set; }
        public Genre()
        {
            MovieGenre = new List<MovieGenre>();
        }
    }
}
