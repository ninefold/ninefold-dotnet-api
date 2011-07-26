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
        readonly string _secret;
        readonly string _userId;
        readonly string _baseUrl;

        public CreateObjectRequest Parameters { get; set; }

        public CreateObject(string userId,
                                        string base64Secret, 
                                        IStorageRequestBuilder requestBuilder, 
                                        IRequestSigningService signingService,
                                        string baseUrl)
        {
            _userId = userId;
            _signingService = signingService;
            _requestBuilder = requestBuilder;
            _secret = base64Secret;
            _baseUrl = baseUrl;
        }

        public ICommandResponse Execute()
        {
            if ((string.IsNullOrWhiteSpace(Parameters.Base64Content) && 
                (!Parameters.ResourcePath[Parameters.ResourcePath.Length].Equals('/'))))
            {
                throw new ArgumentOutOfRangeException("If resource path is specified as an object content length must be non-zero");
            }

            var request = _requestBuilder.GenerateRequest(Parameters, new Uri(new Uri(_baseUrl), Parameters.ResourcePath), _userId, Method.POST);
            var signature = _signingService.GenerateRequestSignature(request, _secret);
            request.Headers.Add("x-emc-signature", signature);

            var response = request.GetResponse();
            //if (response.ErrorException != null) throw new NinefoldApiException(response.ErrorException);

            //return response.Data;
            return new CreateObjectResponse();
        }
    }
}
