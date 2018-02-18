namespace PMDb.Domain.Core
{
    public class MovieDirector
    {
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Director Director { get; set; }
        public int DirectorId { get; set; }
    }
}
