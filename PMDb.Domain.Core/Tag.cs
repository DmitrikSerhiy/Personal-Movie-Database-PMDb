using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieTag> MovieTag { get; set; }
        public Tag()
        {
            MovieTag = new List<MovieTag>();
        }
    }
}
