using System;
using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class DeleteUserMetadata : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public HttpWebRequest Request { get; private set; }

        public DeleteUserMetadataRequest Parameters { get; set; }

        public DeleteUserMetadata(string userId,
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
            if (!Parameters.Resource.PathAndQuery.Contains("metadata/user"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?metadata/user");
            }

            Request = _commandBuilder.GenerateRequest(Parameters, _userId, HttpMethod.DELETE);
            _authenticator.AuthenticateRequest(Request, _secret);
        }

        public ICommandResponse Execute()
        {
            HttpWebResponse response = null;
 
            try
            {
                response = (HttpWebResponse) Request.GetResponse();
                return new DeleteUserMetadataResponse
                {
                    Delta = response.Headers["x-emc-delta"],
                    Policy = response.Headers["x-emc-policy"]
                };
            }
            catch (WebException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return new DeleteUserMetadataResponse()
                               {
                                   ErrorMessage = ex.Message
                               };
                }
                throw;
            }
        }
    }
}
