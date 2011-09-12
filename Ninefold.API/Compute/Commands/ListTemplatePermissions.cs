using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute.Commands
{
    public class ListTemplatePermissions : ICommand
    {
        readonly IComputeCommandAuthenticator _authenticator;
        readonly IComputeRequestBuilder _builder;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public ListTemplatePermissionsRequest Parameters { get; set; }

        public ListTemplatePermissions(string apiKey, string secret, string baseUri, IComputeCommandAuthenticator authenticator, IComputeRequestBuilder builder)
        {
            _apiKey = apiKey;
            _secret = secret;
            _baseUri = baseUri;
            _authenticator = authenticator;
            _builder = builder;
        }

        public HttpWebRequest Prepare()
        {
            return (HttpWebRequest)_builder.GenerateRequest(Parameters, _authenticator, _baseUri, _apiKey, _secret);
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            var responseStream = webResponse.GetResponseStream();
            if ((responseStream == null) || (!responseStream.CanRead)) return new ListTemplatePermissionsResponse();

            var responseDocument = XDocument.Load(responseStream);
            var templatePermission = responseDocument.Elements().Elements()
                    .Where(e => e.Name.LocalName.Equals("templatePermission", StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();

            if (templatePermission == null) return new ListTemplatePermissionsResponse();

            return new ListTemplatePermissionsResponse
                          {
                              Id = int.Parse(templatePermission.ExtractValue("id")),
                              Accounts = templatePermission.ExtractValue("accounts"),
                              DomainId = int.Parse(templatePermission.ExtractValue("domainId")),
                              IsPublic = bool.Parse(templatePermission.ExtractValue("isPublic"))
                          };
        }
    }
}
