
namespace PMDb.Domain.Core
{
    public class MovieWriter
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Writer Writer { get; set; }
        public int WriterId { get; set; }
    }
}
