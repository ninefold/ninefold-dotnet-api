using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine
    {
        readonly INinefoldService _computeService;
        readonly byte[] _secret;

        public string ApiKey { get; set; }
        public string MachineId { get; set; }
        
        public StartVirtualMachine()
        {
            _computeService = new ComputeService("http://tempuri.org/");
            _secret = new byte[] {0x0, 0x1};
        }

        public StartVirtualMachine(INinefoldService ninefoldService, byte[] secret)
        {
            _computeService = ninefoldService;
            _secret = secret;
        }

        public MachineResponse Execute()
        {
            if (string.IsNullOrWhiteSpace(ApiKey)) throw new ArgumentNullException("ApiKey");
            if (string.IsNullOrWhiteSpace(MachineId)) throw new ArgumentNullException("MachineId");

            var request = BuildRequest();
            SignRequest(request);

            return _computeService.ExecuteRequest<MachineResponse>(request);
        }

        private void SignRequest(RestRequest request)
        {
            //BuildUri will html encode params....
            var uri = _computeService.Client.BuildUri(request);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(_secret);
            var signature = hashingAlg.ComputeHash(Encoding.UTF8.GetBytes(uri.ToString()));
            request.AddParameter("signature", signature);
        }

        private RestRequest BuildRequest()
        {
            var requestParams = new Dictionary<string, string>
                                    {
                                        {"command", "startvirtualmachine"},
                                        {"apikey", ApiKey },
                                        {"machineid", MachineId}
                                    }.OrderBy(p => p.Key);
            
            var request = new RestRequest("", Method.POST);
            foreach (var param in requestParams)
            {
                request.AddUrlSegment(param.Key, param.Value.Replace("+", "%20"));
            }

            return request;
        }
    }
}
