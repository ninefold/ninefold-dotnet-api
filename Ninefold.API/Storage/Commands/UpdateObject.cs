using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class UpdateObject : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public HttpWebRequest Request { get; private set; }

        public UpdateObjectRequest Parameters { get; set; }

        public UpdateObject(string userId,
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
            Request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.PUT);
            _authenticator.AuthenticateRequest(Request, _secret);

            var contentStream = Request.GetRequestStream();

            if (Parameters.Content != null)
            {
                contentStream.Write(Parameters.Content, 0, Parameters.Content.Length);
            }
        }

        public ICommandResponse Execute()
        {
            var response = Request.GetResponse();

            return new UpdateObjectResponse
            {
                Delta = long.Parse(response.Headers["x-emc-delta"]),
                Location = response.Headers["location"],
                Policy = response.Headers["x-emc-policy"]
            };
        }
    }
}
