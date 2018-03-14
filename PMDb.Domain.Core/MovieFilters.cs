using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class MovieFilters
    {
        public IList<int?> Year { get; set; } = null;
        public IList<string> Actor { get; set; } = null;
        public IList<string> Director { get; set; } = null;
        public IList<string> Genre { get; set; } = null;
        public IList<string> Writer { get; set; } = null;
        public IList<string> Tag { get; set; } = null;
        public IList<double?> OwnRating { get; set; } = null;
        public IList<double?> IMDbRating { get; set; } = null;
        public IList<double?> MetaCriticRating { get; set; } = null;
        public IList<double?> RotenTomatosRating { get; set; } = null;
    }
}
