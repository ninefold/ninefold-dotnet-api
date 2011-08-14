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

        public HttpWebRequest Request { get; private set; }

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

        public void Prepare()
        {
            Request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.DELETE);
            _authenticator.AuthenticateRequest(Request, _secret);
        }

        public ICommandResponse Execute()
        {
            var response = Request.GetResponse();

            return new DeleteObjectResponse
                       {
                           Delta = long.Parse(response.Headers["x-emc-delta"]),
                           Policy = response.Headers["x-emc-policy"]
                       };
        }
    }
}
