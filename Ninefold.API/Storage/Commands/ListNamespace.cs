using System.IO;
using System.Net;
using System.Xml.Linq;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class ListNamespace : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public ListNamespaceRequest Parameters { get; set; }

        public ListNamespace(string userId,
                                        string base64Secret, 
                                        IStorageCommandBuilder commandBuilder, 
                                        IStorageCommandAuthenticator authenticator)
        {
            _userId = userId;
            _authenticator = authenticator;
            _commandBuilder = commandBuilder;
            _secret = base64Secret;
        }

        public HttpWebRequest Prepare()
        {
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Get);
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            var listResponse = new ListNamespaceResponse
            {
                GroupAcl = response.Headers["x-emc-groupacl"],
                UserAcl = response.Headers["x-emc-useracl"],
                Policy = response.Headers["x-emc-policy"],
                Meta = response.Headers["x-emc-meta"],
            };

            var responseStream = response.GetResponseStream();
            if ((responseStream != null) && (responseStream.CanRead))
            {
                var reader = new StreamReader(responseStream);
                listResponse.Content = XDocument.Load(reader);
            }

            return listResponse;
        }
    }
}
