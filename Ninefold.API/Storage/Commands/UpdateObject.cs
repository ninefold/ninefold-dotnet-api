using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class UpdateObject : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

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

        public HttpWebRequest Prepare()
        {
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Put);
            if (!string.IsNullOrWhiteSpace(Parameters.RangeSpecification))
            {
                request.Headers.Add("range", Parameters.RangeSpecification);
            }
            
            _authenticator.AuthenticateRequest(request, _secret);

            var contentStream = request.GetRequestStream();

            if (Parameters.Content != null)
            {
                contentStream.Write(Parameters.Content, 0, Parameters.Content.Length);
            }

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            return new UpdateObjectResponse
            {
                Delta = long.Parse(response.Headers["x-emc-delta"]),
                Location = response.Headers["location"],
                Policy = response.Headers["x-emc-policy"]
            };
        }
    }
}
