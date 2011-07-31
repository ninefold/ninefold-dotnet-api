using System;
using System.IO;
using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class ListNamespace : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public HttpWebRequest Request { get; private set; }

        public ListNamespaceRequest Parameters { get; set; }

        public ListNamespace(string userId,
                                        string base64Secret, 
                                        IStorageCommandBuilder commandBuilder, 
                                        ICommandAuthenticator authenticator)
        {
            _userId = userId;
            _authenticator = authenticator;
            _commandBuilder = commandBuilder;
            _secret = base64Secret;
        }

        public void Prepare()
        {
            Request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.GET);
            _authenticator.AuthenticateRequest(Request, _secret);
        }

        public ICommandResponse Execute()
        {
            var response = Request.GetResponse();
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
                listResponse.Content = reader.CurrentEncoding.GetBytes(reader.ReadToEnd());
            }

            return listResponse;
        }
    }
}
