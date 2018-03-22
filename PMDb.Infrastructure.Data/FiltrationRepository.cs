using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace PMDb.Infrastructure.Data
{
    public class FiltrationRepository : IFiltrationRepository
    {
        private MovieContext context;
        private FilterTransformer filterTransformer;
        private FilterChecker filterChecker;
        private List<Movie> FiltredMovies = new List<Movie>();

        public FiltrationRepository(MovieContext Context,
            FilterTransformer FilterTransformer,
            FilterChecker FilterChecker)
        {
            context = Context;
            filterTransformer = FilterTransformer;
            filterChecker = FilterChecker;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IList<Movie> Filter(MovieFilters filters)
        {
            filterTransformer.Transform(filters);
            var tFilters = filterTransformer.TransformedFilters;

            foreach (var filter in tFilters)
            {
                var movies = filterChecker
                    .CheckFilter(filter.Item1, filter.Item2, filter.Item3);
                if (FiltredMovies.Count != 0)
                    FiltredMovies = FiltredMovies.Intersect(movies).ToList();
                else
                    FiltredMovies.AddRange(movies);
            }
            return FiltredMovies;
        }

        public IQueryable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int movieId)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movieId) == null ? false : true;
        }

        public bool IsExist(string movieName)
        {
            return context.Movies.FirstOrDefault(m => m.Title == movieName) == null ? false : true;
        }
    }
}
