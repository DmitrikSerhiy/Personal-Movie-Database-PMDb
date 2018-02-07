using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.BusinessModels;
using System;

namespace PMDb.Services.Mappers
{
    public class MapMovieToMovieModel
    {
        public MapMovieToMovieModel()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Movie, MovieModel>());      
        }
        public MovieModel Map()
        {
            return null;
            //return Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repo.GetAll());
        }
    }
}
