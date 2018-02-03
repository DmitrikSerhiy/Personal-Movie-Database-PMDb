using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMDb.Infrastructure.Data
{
    public class MovieRepository : IMovieRepository

    {
        private MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }
        public void AddMark(double mark)
        {
            throw new NotImplementedException();
        }

        public void DeleteMark(int movieId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Movie GetMovie(int movieId)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == movieId);
        }

        public IEnumerable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateMark(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
