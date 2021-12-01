using System.Collections.Generic;
using System.Linq;

namespace Jror.Backend.Libs.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> destination,
            IEnumerable<T> source)
        {
            if (source is null) { return; }

            var enumerable = source.ToList();
            if (!enumerable.Any()) { return; }

            if (destination is List<T> list)
            {
                list.AddRange(enumerable);
            }
            else
            {
                foreach (T item in enumerable)
                {
                    destination.Add(item);
                }
            }
        }
    }
}