using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute.Commands
{
    public class ListServiceOfferings : ICommand
    {
        readonly IComputeCommandAuthenticator _authenticator;
        readonly IComputeRequestBuilder _builder;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public ListServiceOfferingsRequest Parameters { get; set; }

        public ListServiceOfferings(string apiKey, string secret, string baseUri, IComputeCommandAuthenticator authenticator, IComputeRequestBuilder builder)
        {
            _apiKey = apiKey;
            _secret = secret;
            _baseUri = baseUri;
            _authenticator = authenticator;
            _builder = builder;
        }

        public HttpWebRequest Prepare()
        {
            return (HttpWebRequest) _builder.GenerateRequest(Parameters, _authenticator, _baseUri, _apiKey, _secret);
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            var response = new ListServiceOfferingsResponse();
            var responseStream = webResponse.GetResponseStream();
            if ((responseStream != null) && (responseStream.CanRead))
            {
                var responseDocument = XDocument.Load(responseStream);
                response.ServiceOfferings =
                    responseDocument.Root.Elements()
                        .Where(e => e.Name.LocalName.Equals("serviceOffering", StringComparison.InvariantCultureIgnoreCase))
                        .Select(ServiceOffering.From);
            }

            return response;
        }
    }
}
