using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class SetUserMetadata : ICommand
    {

        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public SetUserMetadataRequest Parameters { get; set; }
        
        public SetUserMetadata(string userId,
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
            if (string.IsNullOrWhiteSpace(Parameters.Tags) && (string.IsNullOrWhiteSpace(Parameters.ListableTags)))
            {
                throw new ArgumentOutOfRangeException("Either tags or listable tags must be supplied to the SetUserMetadata command");
            }

            if (!Parameters.Resource.PathAndQuery.Contains("metadata/user"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?metadata/user");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Post);
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            return new SetUserMetadataResponse
                       {
                           Policy = webResponse.Headers["x-emc-policy"]
                       };
        }
    }
}
