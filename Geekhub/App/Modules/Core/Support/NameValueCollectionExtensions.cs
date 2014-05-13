using System.Collections.Generic;
using System.Collections.Specialized;

namespace Geekhub.App.Modules.Core.Support
{
    public static class NameValueCollectionExtensions
    {
        public static string[] GetValuesFrom(this NameValueCollection coll, params string[] names)
        {
            var items = new List<string>();

            foreach (var name in names) {
                var values = coll.GetValues(name);
                if (values != null) {
                    items.AddRange(values);
                }
            }

            return items.ToArray();
        }
    }
}