using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class FiltrationRepository : IFiltrationRepository
    {
        private MovieContext context;
        FilterTransformer filterTransformer;
        

        public FiltrationRepository(MovieContext Context, FilterTransformer FilterTransformer)
        {
            context = Context;
            filterTransformer = FilterTransformer;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Movie Filter(MovieFilters filters)
        {
            filterTransformer.Transform(filters);
            var Tfilters = filterTransformer.TransformedFilters;

            var filter = Tfilters[0];
            Type type = Tfilters[0].Item2.GetType();

            //var exp = FiltrationQueryBuilder.GetExpression(Tfilters[0]);


            var query = FilterQueryBuilder.BuildQuery<Movie>(filter);
            var FiltredMovies = context.Movies.Where(query);

            //var FiltredTwice = FiltredMovies.Where();

            throw new NotImplementedException();
        }


        public IQueryable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int movieId)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movieId) == null ? false : true;
        }

        public bool IsExist(string movieName)
        {
            return context.Movies.FirstOrDefault(m => m.Title == movieName) == null ? false : true;
        }

        //that's may be used later !!!!!YAGNI!!!!
        //private string GetExistedFilter(string name)
        //{
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        var filtersArray = filters.GetType().GetProperties();
        //        var Name = name.First().ToString().ToUpper() + name.Substring(1);
        //        for (int i = 0; i < filtersArray.Length; i++)
        //        {
        //            if (filtersArray[i].Name == Name)
        //                return Name;
        //        }
        //        return null;
        //    }
        //    return null;
        //}
    }
}
