using Microsoft.EntityFrameworkCore;
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
        private List<Movie> FiltredMovies;

        public FiltrationRepository(MovieContext Context,
            FilterTransformer FilterTransformer,
            FilterChecker FilterChecker)
        {
            context = Context;
            filterTransformer = FilterTransformer;
            filterChecker = FilterChecker;
            FiltredMovies = new List<Movie>();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void Filter(MovieFilters filters)
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
        }

        public IQueryable<Movie> GetMovies()
        {
            return FiltredMovies.AsQueryable()
                    .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                    .Include(r => r.Rating)
                    .OrderBy(m => m.Title);
        }

        public bool IsExist(int movieId)
        {
            if(FiltredMovies != null)
                return FiltredMovies.AsQueryable()
                    .FirstOrDefault(m => m.Id == movieId) == null ? false : true;
            return false;
        }

        public bool IsExist(string movieName)
        {
            if (FiltredMovies != null)
                return FiltredMovies.AsQueryable()
                    .FirstOrDefault(m => m.Title == movieName) == null ? false : true;
            return false;
        }
    }
}
