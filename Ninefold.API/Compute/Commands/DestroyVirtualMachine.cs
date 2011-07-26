using System.Net;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DestroyVirtualMachine : ICommand
    {
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly IComputeRequestBuilder _computeRequestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public string MachineId { get; set; }

        public DestroyVirtualMachine(string apiKey, 
                                                        string base64Secret,
                                                        IRequestSigningService signingService, 
                                                        IComputeRequestBuilder computeRequestService, 
                                                        IRestClient client)
        {
            _signingService = signingService;
            _client = client;
            _computeRequestService = computeRequestService;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
        }

        public ICommandResponse Execute()
        {
            var request = _computeRequestService.GenerateRequest(null, _apiKey);
            var signature = _signingService.GenerateRequestSignature(WebRequest.Create(""), _base64Secret);//((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            request.AddUrlSegment("signature", signature);

            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }
    }
}
