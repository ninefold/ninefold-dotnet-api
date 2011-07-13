using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DeployVirtualMachine 
    {
        readonly string _apiKey;
        readonly byte[] _secret;
        readonly INinefoldService _computeService;

        public IDictionary<string, string> Parameters { get; set; }

        public DeployVirtualMachine(string apiKey, byte[] secret, INinefoldService computeService)
        {
            _apiKey = apiKey;
            _computeService = computeService;
            _secret = secret;
        }

        public MachineResponse Execute()
        {
            var request = BuildRequest();
            var uri = _computeService.Client.BuildUri(request);
            SignRequest(request);

            return _computeService.ExecuteRequest<MachineResponse>(request);
        }

        private RestRequest BuildRequest()
        {
            var requestParams = new Dictionary<string, string>
                                    {
                                        {"command", "startvirtualmachine"},
                                        {"apikey", _apiKey }
                                    }.OrderBy(p => p.Key);

            var request = new RestRequest(string.Empty, Method.POST);
            foreach (var param in requestParams)
            {
                request.AddUrlSegment(param.Key, param.Value);
            }

            return request;
        }

        private void SignRequest(RestRequest request)
        {
            var uri = _computeService.Client.BuildUri(request);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(_secret);
            var signature = hashingAlg.ComputeHash(Encoding.UTF8.GetBytes(uri.ToString()));
            request.AddHeader("x-emc-signature", Encoding.UTF8.GetString(signature));
        }
    }
}
