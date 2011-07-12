using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DeployVirtualMachine : ICommand<MachineResponse>
    {
        readonly string _apiKey;
        readonly byte[] _secret;
        readonly INinefoldService _computeService;

        public IDictionary<string, string> OptionalParameters { get; set; }

        public DeployVirtualMachine(string apiKey, byte[] secret, INinefoldService computeService)
        {
            _apiKey = apiKey;
            _computeService = computeService;
            _secret = secret;
        }

        public MachineResponse Execute()
        {
            var request = new RestRequest(string.Empty, Method.POST);
            //map dictionary of params in
            //sign request


            return _computeService.ExecuteRequest<MachineResponse>(request);
        }
    }
}
