using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Ninefold.Compute
{
    internal class ComputeCommandBuilder : IComputeRequestBuilder
    {
        private const BindingFlags PropertyFilters = BindingFlags.Public | BindingFlags.Instance;

        public WebRequest GenerateRequest(IComputeCommandRequest request, IComputeCommandAuthenticator authenticator, string baseUri,  string apiKey, string secret)
        {
            Validator.ValidateObject(request, new ValidationContext(request, null, null), true);

            var requestType = request.GetType();
            
            var queryStringParameters = requestType.GetProperties(PropertyFilters)
                .Where(p => (p.GetValue(request, null) != null) 
                                        && (!string.IsNullOrWhiteSpace(p.GetValue(request, null).ToString()))
                                        && (!p.Name.Equals("command", StringComparison.InvariantCultureIgnoreCase)))
                .Select(p =>
                    new KeyValuePair<string, string>((char.ToLower(p.Name[0]) + p.Name.Remove(0, 1)), p.GetValue(request, null).ToString()))
                .ToList()
                .OrderBy(pair => pair.Key)
                .Aggregate(string.Empty, (query, arg) => string.Format("{0}&{1}={2}", query, arg.Key, arg.Value));

            var queryString = string.Format("apiKey={0}&command={1}{2}", apiKey, request.Command, queryStringParameters);
            var signature = authenticator.AuthenticateRequest(queryString, secret);
            queryString = string.Format("?{0}&signature={1}", queryString, signature);

            return WebRequest.Create(new Uri(new Uri(baseUri), queryString));
        }
    }
}
