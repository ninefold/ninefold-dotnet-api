using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public class ComputeRequestBuilder : IRequestBuilder
    {
        public IRestRequest GenerateRequest(ICommandRequest request, string apiKey)
        {
            Validator.ValidateObject(request, new ValidationContext(request, null, null), true);

            var requestType = request.GetType();
            var restRequest = new RestRequest(string.Empty, Method.POST);
            var properties = requestType.GetProperties(BindingFlags.Public)
                .Select(p =>
                    new KeyValuePair<string, string>(p.Name.ToLowerInvariant(), p.GetValue(request, null).ToString()))
                .ToList();
            properties.Add(new KeyValuePair<string, string>("apikey", apiKey));
                                                        
            var orderedProperties = properties.OrderBy(p => p.Key);

            foreach (var property in orderedProperties)
            {
                restRequest.AddUrlSegment(property.Key, property.Value);
            }

            return restRequest;
        }
    }
}
