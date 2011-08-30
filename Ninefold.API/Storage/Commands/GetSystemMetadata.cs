using System;
using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class GetSystemMetadata : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public GetSystemMetadataRequest Parameters { get; set; }

        public GetSystemMetadata(string userId, string secret, IStorageCommandBuilder commandBuilder, ICommandAuthenticator authenticator)
        {
            _commandBuilder = commandBuilder;
            _userId = userId;
            _secret = secret;
            _authenticator = authenticator;
        }

        public HttpWebRequest Prepare()
        {
            if (!Parameters.Resource.PathAndQuery.Contains("metadata/system"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?metadata/system");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.GET);
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            return new GetSystemMetadataResponse
                       {
                            Metadata = webResponse.Headers["x-emc-meta"],
                            Policy = webResponse.Headers["x-emc-policy"]
                       };
        }
    }
}
