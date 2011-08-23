using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class GetObjectACL : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public GetObjectAclRequest Parameters { get; set; }

        public GetObjectACL(string userId,
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
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.GET);
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
