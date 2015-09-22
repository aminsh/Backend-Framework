using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ToList().ForEach(action.Invoke);
        }
    }
}
