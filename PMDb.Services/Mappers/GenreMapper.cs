using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class GenreMapper
    {
        public static List<GenreModel> Map(List<MovieGenre> Genres)
        {
            return Mapper.Map<MovieGenre[], List<GenreModel>>(Genres.ToArray());
        }

        public static List<MovieGenre> Map(List<GenreModel> Genres)
        {
            return Mapper.Map<GenreModel[], List<MovieGenre>>(Genres.ToArray());
        }
    }
}
