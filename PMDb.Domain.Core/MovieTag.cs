using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class MovieTag
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
