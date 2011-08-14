using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class DeleteObject : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public DeleteObjectRequest Parameters { get; set; }

        public DeleteObject(string userId,
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
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.DELETE);
            _authenticator.AuthenticateRequest(request, _secret);
            
            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            return new DeleteObjectResponse
                       {
                           Delta = long.Parse(response.Headers["x-emc-delta"]),
                           Policy = response.Headers["x-emc-policy"]
                       };
        }
    }
}
