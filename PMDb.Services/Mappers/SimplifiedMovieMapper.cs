using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public class SimplifiedMovieMapper
    {
        public static SimplifiedMovieModel Map(Movie movie)
        {
            var simplifiedMovieModel = Mapper.Map<SimplifiedMovieModel>(movie);
            simplifiedMovieModel.Tags = TagMapper.Map(movie.MovieTag);

            return simplifiedMovieModel;
        }
    }
}
