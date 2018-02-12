using System.Collections.Generic;

namespace PMDb.Domain.Core
{
    public class Writer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieWriter> MovieWriter { get; set; }
        public Writer()
        {
            MovieWriter = new List<MovieWriter>();
        }
    }
}
