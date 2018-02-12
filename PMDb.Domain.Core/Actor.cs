using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieActor> MovieActor { get; set; }
        public Actor()
        {
            MovieActor = new List<MovieActor>();
        }
    }
}
