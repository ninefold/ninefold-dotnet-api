using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class DeleteUserMetadata : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public DeleteUserMetadataRequest Parameters { get; set; }

        public DeleteUserMetadata(string userId,
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
            if (!Parameters.Resource.PathAndQuery.Contains("metadata/user"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?metadata/user");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, "DELETE");
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            return new DeleteUserMetadataResponse
            {
                Delta = response.Headers["x-emc-delta"],
                Policy = response.Headers["x-emc-policy"]
            };
        }
    }
}
