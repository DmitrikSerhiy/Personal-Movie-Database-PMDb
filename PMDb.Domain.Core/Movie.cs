using System;

namespace PMDb.Domain.Core
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Mark { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
    }
}
