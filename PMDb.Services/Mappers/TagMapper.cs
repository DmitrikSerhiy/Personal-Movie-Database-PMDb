using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class TagMapper
    {
        public static List<TagModel> Map(List<MovieTag> Tags)
        {
            return Mapper.Map<MovieTag[], List<TagModel>>(Tags.ToArray());
        }
    }
}
