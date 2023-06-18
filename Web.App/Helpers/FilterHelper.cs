using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Web.App.Helpers
{
    public class FilterHelper
    {
        public static Expression<Func<T, bool>> CreateFilter<T>(T filterObject)//Gönderilen modele göre filtre oluşturur
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression expression = null;
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                object value = propertyInfo.GetValue(filterObject);
                if (value != null)
                {
                    var property = Expression.Property(parameter, propertyInfo);
                    var constant = Expression.Constant(value);
                    var equal = Expression.Equal(property, constant);

                    if (expression == null)
                    {
                        expression = equal;
                    }
                    else
                    {
                        expression = Expression.And(expression, equal);
                    }
                }
            }
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
    }
}