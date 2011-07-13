using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine : ICommand
    {
        readonly IRequestSigningService _signingService;
        readonly IRestClient _client;
        readonly IRequestBuilder _requestService;
        readonly string _apiKey;
        readonly string _base64Secret;
        readonly string _machineId;

        public StartVirtualMachine(string apiKey, 
                                                    string base64Secret, 
                                                    string machineId, 
                                                    string serviceUrlRoot, 
                                                    IRequestSigningService signingService, 
                                                    IRequestBuilder requestService)
        {
            _machineId = machineId;
            _requestService = requestService;
            _signingService = signingService;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
            _client = new RestClient();
        }

        public ICommandResponse Execute()
        {
            var requestParams = new Dictionary<string, string>
                                    {
                                        {"command", "startvirtualmachine"},
                                        {"apikey", _apiKey}
                                    };

            var request = _requestService.GenerateRequest(requestParams);
            var signature = _signingService.GenerateRequestSignature(((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            request.AddUrlSegment("signature", signature);

            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }
    }
}
