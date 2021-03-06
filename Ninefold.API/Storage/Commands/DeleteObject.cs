﻿using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class DeleteObject : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly IStorageCommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public DeleteObjectRequest Parameters { get; set; }

        public DeleteObject(string userId,
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
            var request = _commandBuilder.GenerateRequest(Parameters, _userId, "DELETE");
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
