using PMDb.Domain.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PMDb.Services.Helpers
{
    public class FilterTransformer
    {
        private MovieFilters filters;
        private List<(object, string)> listOfTuplesWithFilters { get; set; }
        public List<(object, string)> TransformedFilters
        {
            get => listOfTuplesWithFilters.Count != 0 ? listOfTuplesWithFilters : null;
        }
        public FilterTransformer(MovieFilters Filters)
        {
            listOfTuplesWithFilters = new List<(object, string)>();
            filters = Filters;
        }

        public void Transform(MovieFilters filters)
        {
            foreach (var property in new List<PropertyInfo>(filters.GetType().GetProperties()))
            {
                var value = property.GetValue(filters, null);
                if (value != null)
                {
                    AddFilterTyplesToList(CastItemsToObject(value as IList),
                        property.Name);
                }
            }
        }

        private IList<object> CastItemsToObject(IList listToCast)
        {
            if (listToCast != null)
                return listToCast.Cast<object>().ToList();
            return null;
        }

        private void AddFilterTyplesToList(IList<object> filters, string name)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                    listOfTuplesWithFilters.Add((filter, name));
            }
        }

    }
}
