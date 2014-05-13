using System.Collections.Generic;

namespace Geekhub.App.Modules.Core.Support
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> lst, IEnumerable<T> items)
        {
            foreach (var item in items) {
                lst.Add(item);
            }
        }
    }
}