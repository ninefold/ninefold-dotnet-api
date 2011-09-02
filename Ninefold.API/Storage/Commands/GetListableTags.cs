using System;
using System.Net;
using Ninefold.Core;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage.Commands
{
    public class GetListableTags : ICommand
    {
        readonly IStorageCommandBuilder _commandBuilder;
        readonly ICommandAuthenticator _authenticator;
        readonly string _secret;
        readonly string _userId;

        public GetListableTags(string userId, string secret, IStorageCommandBuilder commandBuilder, ICommandAuthenticator authenticator)
        {
            _commandBuilder = commandBuilder;
            _userId = userId;
            _secret = secret;
            _authenticator = authenticator;
        }

        public GetListableTagsRequest Parameters { get; set; }

        public HttpWebRequest Prepare()
        {
            if (!Parameters.Resource.PathAndQuery.Contains("listabletags"))
            {
                Parameters.Resource = new Uri(Parameters.Resource, "?listabletags");
            }

            var request = _commandBuilder.GenerateRequest(Parameters, _userId, WebRequestMethods.Http.Get);
            _authenticator.AuthenticateRequest(request, _secret);

            return request;
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            return new GetListableTagsResponse
                       {
                           Policy = webResponse.Headers["x-emc-policy"],
                           Tags = webResponse.Headers["x-emc-listable-tags"],
                           Token = webResponse.Headers["x-emc-token"] ?? string.Empty
                       };
        }
    }
}
