﻿using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class FilterChecker
    {
        private MovieContext context;
        private IDictionary<string, IQueryable> dbSets;

        public FilterChecker(MovieContext Context)
        {
            context = Context;
            InitDictionary();
        }
        private void InitDictionary()
        {
            var RatingEntity = context.Ratings;
            var MovieEntity = context.Movies;
            dbSets = new Dictionary<string, IQueryable>()
            {
                {nameof(Actor), context.Actors},
                {nameof(Director), context.Directors},
                {nameof(Genre), context.Genres},
                {nameof(Movie.Year), MovieEntity},
                {nameof(Movie.Runtime), MovieEntity},
                {nameof(Movie), MovieEntity},
                {nameof(Writer), context.Writers},
                {nameof(Tag), context.Tags},
                {nameof(Rating.OwnRating), RatingEntity},
                {nameof(Rating.IMDbRating), RatingEntity},
                {nameof(Rating.MetaCriticRating), RatingEntity},
                {nameof(Rating.RotenTomatosRating), RatingEntity},
            };
               
        }

        private IQueryable GetDbSetValues(string Name)
        {
            return dbSets.Keys.Contains(Name) ? dbSets[Name] : null;
        }

        public IQueryable GetDbSet(string Name)
        {
            var NameForPropery = Name;
            PropertyInfo Prop;
            if (NameForPropery == nameof(Movie.Year) ||
                NameForPropery == nameof(Movie.Runtime))
                NameForPropery = nameof(Movie) + "s";
            else if (NameForPropery == nameof(Rating.OwnRating) ||
                    NameForPropery == nameof(Rating.IMDbRating) ||
                    NameForPropery == nameof(Rating.MetaCriticRating) ||
                    NameForPropery == nameof(Rating.RotenTomatosRating) ||
                     NameForPropery == nameof(Rating))
            {
                NameForPropery = nameof(Rating) + "s";
            }
            else
                NameForPropery = Name + "s";
            Prop = context.GetType().GetProperty(NameForPropery);
            Prop.SetValue(context, GetDbSetValues(Name));

            var dbSet = Prop.GetValue(context);
            return dbSet as IQueryable;
        }

        public IEnumerable<Movie> CheckFilter(object value, string EntityName, string IntermidiateTableName)
        {
            List<Movie> Movies = new List<Movie>();
            var currentDbset = GetDbSet(EntityName);
            var MoviesFromContext = context.Movies;

            if (IntermidiateTableName != "")//many-to-many tables filtrations
            {
                var EntityType = currentDbset.GetType().GetGenericArguments().Single().Name;
                var IntermidiateTable = MoviesFromContext.SelectMany(IntermidiateTableName);
                if (EntityType == nameof(Tag)) { value = "#" + value; }
                var criteries = currentDbset
                    .Where(GetClauseForField(EntityType), value);
                foreach (var filed in criteries)
                    Movies.AddRange(IntermidiateTable.Where(GetClauseForEntity(EntityName), filed).Select(nameof(Movie))
                        .ToDynamicList<Movie>());
            }
            else
            {
                var FieldName = EntityName;
                if (EntityName == nameof(Movie.Year) ||
                    EntityName == nameof(Movie.Runtime))//movie fields filtration

                    Movies.AddRange(currentDbset.Where(GetClauseForField(FieldName), value)
                   .ToDynamicList<Movie>());
                else//Rating filtration
                {
                    var MinMark = (byte)Math.Truncate((double)value);
                    var MaxMark = MinMark + 1;
                    var Ratings = currentDbset.Where(GetClauseForRating(FieldName), MaxMark, MinMark);
                    foreach (var rating in Ratings)
                    {
                        Movies.AddRange(MoviesFromContext.Where(GetClauseForEntity(nameof(Rating)), rating)
                            .ToDynamicList<Movie>());
                    }
                }
            }
            return Movies;
        }

        private string GetClauseForRating(string FieldName)
        {
            return $"it.{FieldName}<=@0 and it.{FieldName}>=@1";
        }

        private string GetClauseForField(string type)
        {
            string currType = null;
            switch (type)
            {
                case nameof(Movie.Year): currType = "it.Year=@0"; break;
                case nameof(Movie.Runtime): currType = "it.Runtime=@0"; break;
                case nameof(Rating.OwnRating): currType = "it.OwnRating=@0"; break;
                case nameof(Rating.IMDbRating): currType = "it.IMDbRating=@0"; break;
                case nameof(Rating.MetaCriticRating): currType = "it.MetaCriticRating=@0"; break;
                case nameof(Rating.RotenTomatosRating): currType = "it.RotenTomatosRating=@0"; break;
                default: currType = "it.Name=@0"; break;
            }
            return currType;
        }

        private string GetClauseForEntity(string EntityType)
        {
            switch (EntityType)
            {
                case "Actor": return "it.Actor=@0";
                case "Director": return "it.Director=@0";
                case "Genre": return "it.Genre=@0";
                case "Writer": return "it.Writer=@0";
                case "Tag": return "it.Tag=@0";
                case "Rating": return "it.Rating=@0";
                default: return null;
            }
        }
    }
}
