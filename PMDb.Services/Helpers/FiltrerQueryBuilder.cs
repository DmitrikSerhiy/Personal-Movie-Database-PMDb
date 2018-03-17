using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class FilterQueryBuilder
    {

        public FilterQueryBuilder()
        {

        }

        public static Func<T, bool> BuildQuery<T>((object, string) filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            MemberExpression member = Expression.Property(param, filters.Item2);
            ConstantExpression constant = Expression.Constant(filters.Item1);
            var exp = Expression.Equal(member, ConvertType(constant));
            return Expression.Lambda<Func<T, bool>>(exp, param).Compile();
        }

        //rewrite this so it return the list of movies


        private static Expression ConvertType(ConstantExpression constant)
        {
            var type = constant.Value.GetType().ToString();
            switch (type)
            {
                case "System.Int32": return Expression.Convert(constant, typeof(int?));
                case "System.Double": return Expression.Convert(constant, typeof(double?));
                case "System.String": return constant;
                default: return null;
            }
        }
    }
}
