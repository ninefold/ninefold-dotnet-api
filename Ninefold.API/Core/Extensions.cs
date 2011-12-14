using System;
using System.Linq;
using System.Xml.Linq;

namespace Ninefold.Core
{
    internal static class Extensions
    {
        public static string ExtractValue(this XContainer element, string fieldName)
        {
            var node = element.Elements()
                .Where(e => e.Name.LocalName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));
            
            return node.Any() ?  node.First().Value : string.Empty;            
        }

        public static T ExtractValue<T>(this XContainer element, string fieldName)
        {
            var node = element.Elements()
                .Where(e => e.Name.LocalName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));

            var value = default(T);

            if (node.Any())
            {
                value = (T) Convert.ChangeType(node.First().Value, typeof(T));
            }

            return value;
        }
    }
}