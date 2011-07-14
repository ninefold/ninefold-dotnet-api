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
            var request = _requestService.GenerateRequest(Parameters, _apiKey);
            var signature = _signingService.GenerateRequestSignature(((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            request.AddUrlSegment("signature", signature);
            
            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }

        private void ValidateRequest()
        {
            if (string.IsNullOrWhiteSpace(Parameters.ServiceOfferingId)) { throw new ArgumentNullException("ServiceOfferingId"); }
            if (string.IsNullOrWhiteSpace(Parameters.TemplateId)) { throw new ArgumentNullException("TemplateId"); }
            if (string.IsNullOrWhiteSpace(Parameters.ZoneId)) { throw new ArgumentNullException("ZoneId"); }

            if (!string.IsNullOrWhiteSpace(Parameters.Account) && string.IsNullOrWhiteSpace(Parameters.DomainId)) { throw new ArgumentNullException("DomainId", "DomainId must be provided when an Account is provided");}
            if (!string.IsNullOrWhiteSpace(Parameters.DiskOfferingId) && (string.IsNullOrWhiteSpace(Parameters.Size))) { throw new ArgumentOutOfRangeException("Either the DiskOfferingId or the Size parameter can be provided");}
            if (!string.IsNullOrWhiteSpace(Parameters.Base64UserData) && (Parameters.Base64UserData.Length > 2048)) {throw new ArgumentOutOfRangeException("Base64UserData cannot be greater than 2KB");}
        }
    }
}
