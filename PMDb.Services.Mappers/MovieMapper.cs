using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.BusinessModels;
using System;

namespace PMDb.Services.Mappers
{
    public class MovieMapper
    {
        public MovieMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Movie, MovieModel>());      
        }

        public static MovieModel Map(Movie movie)
        {
            return Mapper.Map<MovieModel>((movie));
        }
    }
}
