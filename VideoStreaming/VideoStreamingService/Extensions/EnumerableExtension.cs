using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class EnumerableExtension
    {

        public static T SingleOrElse<T>(
            this IEnumerable<T> values,
            Func<T, bool> predicate,
            Func<T> optional)
        {
            try
            {
                return values.Single(predicate);
            }
            catch (Exception)
            {
                return optional.Invoke();
            }
        }
    }
}
