using System;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;
using RestSharp;

namespace Ninefold.API.Storage.Commands
{
    public class CreateObject : ICommand
    {
        readonly IStorageRequestBuilder _requestBuilder;
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly string _secret;
        readonly string _userId;

        public CreateObjectRequest Parameters { get; set; }

        public CreateObject(string userId,
                                        string base64Secret, 
                                        IStorageRequestBuilder requestBuilder, 
                                        IRequestSigningService signingService, 
                                        IRestClient restClient)
        {
            _client = restClient;
            _userId = userId;
            _signingService = signingService;
            _requestBuilder = requestBuilder;
            _secret = base64Secret;
        }

        public ICommandResponse Execute()
        {
            if ((string.IsNullOrWhiteSpace(Parameters.Base64Content) && 
                (!Parameters.ResourcePath[Parameters.ResourcePath.Length].Equals('/'))))
            {
                throw new ArgumentOutOfRangeException("If resource path is specified as an object content length must be non-zero");
            }
            
            var request = _requestBuilder.GenerateRequest(Parameters, Parameters.ResourcePath, _userId, Method.POST);
            var signature = _signingService.GenerateRequestSignature(((RestClient)_client).BuildUri((RestRequest)request), _secret);
            request.AddHeader("x-emc-signature", signature);

            return _client.Execute<CreateObjectResponse>((RestRequest)request).Data;
        }
    }
}
