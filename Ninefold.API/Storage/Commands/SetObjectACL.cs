using System;
using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class SetObjectACL : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public SetObjectACLRequest Parameters { get; set; }

        public SetObjectACL(string userId,
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
            if (!Parameters.Resource.PathAndQuery.Contains("acl"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?acl");    
            }

            if (string.IsNullOrWhiteSpace(Parameters.GroupACL) && (string.IsNullOrWhiteSpace(Parameters.UserACL)))
            {
                throw new ArgumentOutOfRangeException("Either a group acl or a user acl must be supplied to the SetObjectACL command");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.POST);
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            return new SetObjectACLResponse
            {
                Policy = response.Headers["x-emc-policy"]
            };
        }
    }
}
