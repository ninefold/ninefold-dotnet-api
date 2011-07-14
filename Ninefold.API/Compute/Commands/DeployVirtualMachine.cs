using System;
using System.Collections.Generic;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DeployVirtualMachine : ICommand
    {
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly IRequestBuilder _requestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public DeployVirtualMachineRequest Parameters { get; set; }

        public DeployVirtualMachine(string apiKey, 
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
            ValidateRequest();

            var request = _requestService.GenerateRequest(Parameters, _apiKey);
            var uri = Uri.UnescapeDataString(((RestClient) _client).BuildUri((RestRequest) request).ToString());
            var signature = _signingService.GenerateRequestSignature(new Uri(uri), _base64Secret);
            request.AddUrlSegment("signature", signature);
            
            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }

        private void ValidateRequest()
        {
            if (!string.IsNullOrWhiteSpace(Parameters.Account) && string.IsNullOrWhiteSpace(Parameters.DomainId)) { throw new ArgumentNullException("DomainId", "DomainId must be provided when an Account is provided");}
            if (!string.IsNullOrWhiteSpace(Parameters.DiskOfferingId) && (string.IsNullOrWhiteSpace(Parameters.Size))) { throw new ArgumentOutOfRangeException("Either the DiskOfferingId or the Size parameter can be provided");}            
        }
    }
}
