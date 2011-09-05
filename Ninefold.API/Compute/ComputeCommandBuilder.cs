using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using Ninefold.Compute.Commands;

namespace Ninefold.Compute
{
    internal class ComputeCommandBuilder : IComputeRequestBuilder
    {
        private const BindingFlags PropertyFilters = BindingFlags.Public | BindingFlags.Instance;

        public WebRequest GenerateRequest(IComputeCommandRequest request, IComputeCommandAuthenticator authenticator, string baseUri,  string apiKey, string secret)
        {
            Validator.ValidateObject(request, new ValidationContext(request, null, null), true);

            var requestType = request.GetType();
            
            var properties = requestType.GetProperties(PropertyFilters)
                .Where(p => (p.GetValue(request, null) != null) && (!string.IsNullOrWhiteSpace(p.GetValue(request, null).ToString())))
                .Select(p =>
                    new KeyValuePair<string, string>(p.Name.ToLowerInvariant(), Uri.EscapeUriString(p.GetValue(request, null).ToString())))
                .ToList();
            properties.Add(new KeyValuePair<string, string>("apiKey", apiKey));

            var queryStringParams = properties.OrderBy(pair => pair.Key)
                .Aggregate("?", (query, arg) => string.Format("{0}&{1}={2}", query, arg.Key, arg.Value))
                .Replace("?&", "?")
                .ToLower();

            var escapedQueryString = Uri.EscapeUriString(queryStringParams).Replace("+", "%20");
            var signedQueryString = authenticator.AuthenticateRequest(escapedQueryString, secret);
            return WebRequest.Create(new Uri(new Uri(baseUri), signedQueryString));
        }
    }
}
