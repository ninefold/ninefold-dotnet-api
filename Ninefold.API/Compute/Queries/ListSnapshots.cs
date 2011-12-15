using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute.Queries
{
    public class ListSnapshots : ICommand
    {
        readonly IComputeCommandAuthenticator _authenticator;
        readonly IComputeRequestBuilder _builder;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public ListSnapshotsRequest Parameters { get; set; }

        public ListSnapshots(string apiKey, string secret, string baseUri, IComputeCommandAuthenticator authenticator, IComputeRequestBuilder builder)
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
            var response = new ListSnapshotsResponse();
            var responseStream = webResponse.GetResponseStream();
            if ((responseStream != null) && (responseStream.CanRead))
            {
                var responseDocument = XDocument.Load(responseStream);
                response.Snapshots =
                    responseDocument.Root.Elements()
                        .Where(e => e.Name.LocalName.Equals("snapshot", StringComparison.InvariantCultureIgnoreCase))
                        .Select(Snapshot.From);
            }

            return response;
        }
    }
}
