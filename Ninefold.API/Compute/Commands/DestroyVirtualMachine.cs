using System;
using System.Collections.Generic;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DestroyVirtualMachine : ICommand
    {
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly IRequestBuilder _requestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public string MachineId { get; set; }

        public DestroyVirtualMachine(string apiKey, 
                                                        string base64Secret,
                                                        IRequestSigningService signingService, 
                                                        IRequestBuilder requestService, 
                                                        IRestClient client)
        {
            _signingService = signingService;
            _client = client;
            _requestService = requestService;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
        }

        public ICommandResponse Execute()
        {
            

            var request = _requestService.GenerateRequest(null, _apiKey);
            var signature = _signingService.GenerateRequestSignature(((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            request.AddUrlSegment("signature", signature);

            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }

        private void ValidateRequest()
        {
            if (string.IsNullOrWhiteSpace(MachineId)) { throw new ArgumentNullException("MachineId");}
        }
    }
}
