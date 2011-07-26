using System.Net;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine : ICommand
    {
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly IComputeRequestBuilder _computeRequestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public StartVirtualMachineRequest Parameters { get; set; }

        public StartVirtualMachine(string apiKey, 
                                                    string base64Secret, 
                                                    string serviceUrlRoot, 
                                                    IRequestSigningService signingService, 
                                                    IComputeRequestBuilder computeRequestService)
        {
            _computeRequestService = computeRequestService;
            _signingService = signingService;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
            _client = new RestClient(serviceUrlRoot);
        }

        public ICommandResponse Execute()
        {
            var request = _computeRequestService.GenerateRequest(Parameters, _apiKey);
            var signature = _signingService.GenerateRequestSignature(WebRequest.Create(""), _base64Secret);//((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            request.AddUrlSegment("signature", signature);

            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }
    }
}
