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

            return node.Count() > 0 ? node.First().Value : string.Empty;
        }
    }
}