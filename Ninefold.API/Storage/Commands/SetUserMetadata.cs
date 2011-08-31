using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class SetUserMetadata : ICommand
    {

        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public SetUserMetadataRequest Parameters { get; set; }
        
        public SetUserMetadata(string userId,
                                        string base64Secret, 
                                        IStorageCommandBuilder commandBuilder, 
                                        ICommandAuthenticator authenticator)
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
            
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.POST);
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
