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

        public HttpWebRequest Request { get; private set; }

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

        public void Prepare()
        {
            if (!Parameters.Resource.PathAndQuery.Contains("acl"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?acl");    
            }
            Request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.POST);
            _authenticator.AuthenticateRequest(Request, _secret);
        }

        public ICommandResponse Execute()
        {
            var response = Request.GetResponse();

            return new SetObjectACLResponse
            {
                Policy = response.Headers["x-emc-policy"]
            };
        }
    }
}
