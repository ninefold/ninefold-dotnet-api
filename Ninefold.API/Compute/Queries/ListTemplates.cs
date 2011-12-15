using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute.Queries
{
    public class ListTemplates : ICommand
    {
        readonly IComputeCommandAuthenticator _authenticator;
        readonly IComputeRequestBuilder _builder;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public ListTemplates(string apiKey, string secret, string baseUri, IComputeCommandAuthenticator authenticator, IComputeRequestBuilder builder)
        {
            _authenticator = authenticator;
            _secret = secret;
            _apiKey = apiKey;
            _builder = builder;
            _baseUri = baseUri;
        }
        
        public ListTemplatesRequest Parameters { get; set; }
        
        public HttpWebRequest Prepare()
        {
            return (HttpWebRequest)_builder.GenerateRequest(Parameters, _authenticator, _baseUri, _apiKey, _secret);            
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            var response = new ListTemplatesResponse();
            var responseStream = webResponse.GetResponseStream();
            if ((responseStream != null) && (responseStream.CanRead))
            {
                var responseDocument = XDocument.Load(responseStream);
                response.Templates =
                    responseDocument.Root.Elements()
                        .Where(e => e.Name.LocalName.Equals("template", StringComparison.InvariantCultureIgnoreCase))
                        .Select(Template.From);
            }

            return response;
        }
    }
}
