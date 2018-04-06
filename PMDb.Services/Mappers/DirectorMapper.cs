using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class DirectorMapper
    {
        public static List<DirectorModel> Map(List<MovieDirector> Directors)
        {
            return Mapper.Map<MovieDirector[], List<DirectorModel>>(Directors.ToArray());
        }

        public static List<MovieDirector> Map(List<DirectorModel> Directors)
        {
            return Mapper.Map<DirectorModel[], List<MovieDirector>>(Directors.ToArray());
        }
    }
}
