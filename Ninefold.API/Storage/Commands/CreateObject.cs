using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class CreateObject : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public CreateObjectRequest Parameters { get; set; }

        public CreateObject(string userId,
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
            if (Parameters == null) throw new ArgumentException("Parameters");

            if ((Parameters.Content == null || Parameters.Content.Length == 0) &&
                (!Parameters.Resource.PathAndQuery[Parameters.Resource.PathAndQuery.Length - 1].Equals('/')))
            {
                throw new ArgumentOutOfRangeException("If resource path is specified as an object content length must be non-zero");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Post);
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
            return new CreateObjectResponse
                       {
                           Delta = response.Headers["x-emc-delta"],
                           Location = response.Headers["location"],
                           Policy = response.Headers["x-emc-policy"]
                       };
        }
    }
}
