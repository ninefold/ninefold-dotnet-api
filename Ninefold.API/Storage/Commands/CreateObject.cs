using System;
using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;
using RestSharp;

namespace Ninefold.API.Storage.Commands
{
    public class CreateObject : ICommand
    {

        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        readonly string _baseUrl;

        public WebRequest Request { get; private set; }

        public CreateObjectRequest Parameters { get; set; }

        public CreateObject(string userId,
                                        string base64Secret, 
                                        IStorageCommandBuilder commandBuilder, 
                                        ICommandAuthenticator authenticator,
                                        string baseUrl)
        {
            _userId = userId;
            _authenticator = authenticator;
            _commandBuilder = commandBuilder;
            _secret = base64Secret;
            _baseUrl = baseUrl;
        }
        
        public void Prepare()
        {
            if (Parameters == null) throw new ArgumentException("Parameters");

            if ((Parameters.Content == null || Parameters.Content.Length == 0) &&
                (!Parameters.Resource.PathAndQuery[Parameters.Resource.PathAndQuery.Length].Equals('/')))
            {
                throw new ArgumentOutOfRangeException("If resource path is specified as an object content length must be non-zero");
            }

            Request = _commandBuilder.GenerateRequest(Parameters, _userId, Method.POST);
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

            return new CreateObjectResponse
                       {
                           Delta = response.Headers["x-emc-delta"],
                           Location = response.Headers["location"],
                           Policy = response.Headers["x-emc-policy"]
                       };
        }
    }
}
