using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public class ComputeRequestBuilder : IRequestBuilder
    {
        private const BindingFlags PropertyFilters = BindingFlags.Public | BindingFlags.Instance;

        public IRestRequest GenerateRequest(ICommandRequest request, string apiKey)
        {
            Validator.ValidateObject(request, new ValidationContext(request, null, null), true);

            var queryString = new StringBuilder();
            var requestType = request.GetType();
            
            var properties = requestType.GetProperties(PropertyFilters)
                .Where(p => (p.GetValue(request, null) != null) && (!string.IsNullOrWhiteSpace(p.GetValue(request, null).ToString())))
                .Select(p =>
                    new KeyValuePair<string, string>(p.Name.ToLowerInvariant(), p.GetValue(request, null).ToString()))
                .ToList();
            properties.Add(new KeyValuePair<string, string>("apikey", apiKey));
                                                        
            var orderedProperties = properties.OrderBy(p => p.Key);

            foreach (var property in orderedProperties)
            {
                queryString.Append(string.Format("{0}={1}&", property.Key, property.Value));
            }
            queryString.Remove(queryString.Length - 1, 1);

            return new RestRequest("?" + queryString, Method.POST);
        }
    }
}
