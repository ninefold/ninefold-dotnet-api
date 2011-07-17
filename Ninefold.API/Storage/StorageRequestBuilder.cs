using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public class StorageRequestBuilder : IStorageRequestBuilder
    {
        private const BindingFlags PropertyFilters = BindingFlags.Public | BindingFlags.Instance;

        public IRestRequest GenerateRequest(ICommandRequest request, string resource, string userId, Method requestMethod)
        {
            Validator.ValidateObject(request, new ValidationContext(request, null, null), true);

            var restRequest = new RestRequest(resource, requestMethod);
            var requestType = request.GetType();

            var properties = requestType.GetProperties(PropertyFilters)
                .Where(p => (p.GetValue(request, null) != null) && (!string.IsNullOrWhiteSpace(p.GetValue(request, null).ToString())));
            
            foreach (var property in properties)
            {
                var attributeName = property.Name;
                var attributes = property.GetCustomAttributes(false);
                if (attributes.Count() > 0)
                {
                    var nameAttribute = (HeaderAttribute) attributes.FirstOrDefault(attr => (attr as HeaderAttribute) != null);
                    attributeName = nameAttribute.Name;
                }

                restRequest.AddHeader(attributeName, property.GetValue(request, null).ToString());
            }

            restRequest.AddHeader("x-emc-uid", userId);
            return restRequest;
        }
    }
}
