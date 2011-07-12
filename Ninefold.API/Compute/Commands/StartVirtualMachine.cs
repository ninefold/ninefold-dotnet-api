using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine : ICommand<MachineResponse>
    {
        readonly INinefoldService _computeService;
        readonly byte[] _secret;
        readonly string _apiKey;
        readonly string _machineId;
        
        public StartVirtualMachine(INinefoldService ninefoldService, byte[] secret, string apiKey, string machineId)
        {
            _computeService = ninefoldService;
            _machineId = machineId;
            _apiKey = apiKey;
            _secret = secret;
        }

        public MachineResponse Execute()
        {
            if (string.IsNullOrWhiteSpace(_apiKey)) throw new ArgumentNullException("ApiKey");
            if (string.IsNullOrWhiteSpace(_machineId)) throw new ArgumentNullException("MachineId");

            var request = BuildRequest();
            SignRequest(request);

            return _computeService.ExecuteRequest<MachineResponse>(request);
        }

        private RestRequest BuildRequest()
        {
            var requestParams = new Dictionary<string, string>
                                    {
                                        {"command", "startvirtualmachine"},
                                        {"apikey", _apiKey },
                                        {"machineid", _machineId}
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
            request.AddParameter("signature", Encoding.UTF8.GetString(signature));
        }
    }

    public interface ICommand<out TResponse>
    {
        TResponse Execute();
    }
}
