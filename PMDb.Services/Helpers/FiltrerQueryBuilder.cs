using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class FilterQueryBuilder
    {
        private static ParameterExpression param;
        private static Type currentTable;
        public FilterQueryBuilder()
        {

        }

        public static Func<T,bool> BuildQuery<T>((object, string) filters)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            MemberExpression member = Expression.Property(param, filters.Item2);
            ConstantExpression constant = Expression.Constant(filters.Item1);
            var exp = Expression.Equal(member, ConvertType(constant));

            return Expression.Lambda<Func<T, bool>>(exp, param).Compile();
        }

        private static Expression ConvertType(ConstantExpression constant)
        {
            var type = constant.Value.GetType().ToString();
            switch (type)
            {
                case "System.Int32": return Expression.Convert(constant, typeof(int?));
                case "System.Double": return Expression.Convert(constant, typeof(double?));
                default: return null;
            }
        }
    }
}
