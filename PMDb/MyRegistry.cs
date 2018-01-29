using PMDb.Domain.Interfeces;
using PMDb.Infrastucrure.Data;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMDb
{
    public class MyRegistry : Registry
    {
        public MyRegistry(string connectionString)
        {
            For<IMovieRepository>()
                .Transient()
                .Use<MovieRepository>()
                .Ctor<string>().Is(connectionString);

        }
    }
}
