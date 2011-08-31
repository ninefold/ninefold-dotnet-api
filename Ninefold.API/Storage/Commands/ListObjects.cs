using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage.Commands
{
    public class ListObjects : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;
        
        public ListObjectsRequest Parameters { get; set; }

        public ListObjects(string userId,
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

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            var listResponse = new ListObjectsResponse
            {
                Policy = webResponse.Headers["x-emc-policy"],
                Token = webResponse.Headers["x-emc-token"] ?? string.Empty
            };

            var responseStream = webResponse.GetResponseStream();
            if ((responseStream != null) && (responseStream.CanRead))
            {
                var reader = new StreamReader(responseStream);
                listResponse.Content =  XDocument.Load(reader);
            }

            return listResponse;
        }
    }
}
