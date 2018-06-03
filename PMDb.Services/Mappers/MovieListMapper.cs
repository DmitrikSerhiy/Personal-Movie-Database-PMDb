using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class MovieListMapper
    {
        public static MovieListModel Map(MovieList movieList)
        {
            var movieListModel = Mapper.Map<MovieListModel>(movieList);

            for (int i = 0; i < movieListModel.Movies.Count; i++)
            {
                movieListModel.Movies[i].Tags = TagMapper.Map(movieList.MovieListMovies[i].Movie.MovieTag);
            }

            return movieListModel; 
        }
    }
}
