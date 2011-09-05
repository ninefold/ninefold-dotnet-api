using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class GetObjectACL : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public GetObjectAclRequest Parameters { get; set; }

        public GetObjectACL(string userId,
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
            if (!Parameters.Resource.PathAndQuery.Contains("acl"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?acl");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Get);
            _authenticator.AuthenticateRequest(request, _secret);
            
            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            return new GetObjectAclResponse
                       {
                           GroupAcl = response.Headers["x-emc-groupacl"],
                           UserAcl = response.Headers["x-emc-useracl"],
                           Policy = response.Headers["x-emc-policy"]
                       };
        }
    }
}
