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
            return Mapper.Map<MovieListModel>(movieList);
        }
    }
}
