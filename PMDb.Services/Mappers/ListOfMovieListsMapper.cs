using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public class ListOfMovieListsMapper
    {
        public static List<ListOfMovieListsModel> Map(List<MovieListMovie> movieListMovies)
        {
            return Mapper.Map<MovieListMovie[], List<ListOfMovieListsModel>>(movieListMovies.ToArray());
        }

        public static ListOfMovieListsModel Map(MovieListMovie movieListMovies)
        {
            return Mapper.Map<ListOfMovieListsModel>(movieListMovies);
        }
    }
}
