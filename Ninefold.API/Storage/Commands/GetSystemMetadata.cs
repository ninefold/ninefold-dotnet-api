using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class GetSystemMetadata : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public GetSystemMetadataRequest Parameters { get; set; }

        public GetSystemMetadata(string userId, string secret, IStorageCommandBuilder commandBuilder, IStorageCommandAuthenticator authenticator)
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

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Get);
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
