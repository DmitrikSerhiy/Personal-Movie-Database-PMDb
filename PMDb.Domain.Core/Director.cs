using System.Collections.Generic;

namespace PMDb.Domain.Core
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieDirector> MovieDirector { get; set; }
        public Director()
        {
            MovieDirector = new List<MovieDirector>();
        }
    }
}
