using PMDb.Domain.Core;
using PMDb.Domain.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Infrastucrure.Data
{
    
    public class MovieRepository : IMovieRepository
    {
        private MovieContext _movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public double? AddMark(double mark)
        {
            _movieContext.Movies.Add(new Movie
            {
                Director = null,
                Genre = null,
                Name = null,
                Mark = mark
            });
            return mark;
        }

        public void DeleteMark(int movieId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Movie GetMovie(int movieId)
        {
            var movieQ = _movieContext.Movies.Where(m => m.Id == movieId).ToList();
            return new Movie
            {
                Id = movieQ[0].Id,
                Name = movieQ[0].Name,
                Genre = movieQ[0].Genre,
                Mark = movieQ[0].Mark,
                Director = movieQ[0].Director
            };
        }

        public IEnumerable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public double? UpdateMark(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
