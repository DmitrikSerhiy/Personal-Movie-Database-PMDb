using PMDb.Domain.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PMDb.Infrastructure.Data
{
    public class FilterTransformer
    {
        private MovieFilters filters;
        private List<(object, string, string)> listOfTuplesWithFilters { get; set; }
        public List<(object, string, string)> TransformedFilters
        {
            get => listOfTuplesWithFilters.Count != 0 ? listOfTuplesWithFilters : null;
        }
        public FilterTransformer(MovieFilters Filters)
        {
            listOfTuplesWithFilters = new List<(object, string, string)>();
            filters = Filters;
        }

        public void Transform(MovieFilters filters)
        {
            foreach (var property in new List<PropertyInfo>(filters.GetType().GetProperties()))
            {
                var value = property.GetValue(filters, null);
                if (value != null)
                {
                    var Name = property.Name;
                    if (Name == nameof(Movie.Year) ||
                        Name == nameof(Rating.OwnRating) ||
                        Name == nameof(Rating.IMDbRating) ||
                        Name == nameof(Rating.MetaCriticRating) ||
                        Name == nameof(Rating.RotenTomatosRating))
                    {
                        AddFilterTyplesToList(CastItemsToObject(value as IList),
                            Name, "");
                    }
                    else
                        AddFilterTyplesToList(CastItemsToObject(value as IList),
                            Name, "Movie" + Name);
                }
            }
        }

        private IList<object> CastItemsToObject(IList listToCast)
        {
            if (listToCast != null)
                return listToCast.Cast<object>().ToList();
            return null;
        }

        private void AddFilterTyplesToList(IList<object> filters, string name, string IntermidiateTableName)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                    listOfTuplesWithFilters.Add((filter, name, IntermidiateTableName));
            }
        }

    }
}
