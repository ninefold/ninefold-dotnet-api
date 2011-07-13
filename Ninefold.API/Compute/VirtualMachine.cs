using System;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;

namespace Ninefold.API.Compute
{
    public class VirtualMachine : IVirtualMachine
    {
        readonly string _apiKey;
        readonly string _base64Secret;
        readonly string _serviceUrlRoot;
        readonly IRequestBuilder _requestBuilder;
        readonly IRequestSigningService _requestSigner;
        
        public VirtualMachine(string apiKey, string base64Secret, string serviceUrlRoot)
        {
            _apiKey = apiKey;
            _serviceUrlRoot = serviceUrlRoot;
            _base64Secret = base64Secret;
        }

        public MachineResponse Deploy()
        {
            var command = new DeployVirtualMachine(_apiKey, _base64Secret,  _serviceUrlRoot,_requestSigner, _requestBuilder);
            return (MachineResponse) command.Execute();
        }

        public MachineResponse Start(string machineId)
        {
            var command = new StartVirtualMachine(_apiKey, _base64Secret, machineId, _serviceUrlRoot, _requestSigner, _requestBuilder);
            return (MachineResponse) command.Execute();
        }

        public MachineResponse Stop()
        {
            throw new NotImplementedException();
        }
    }
}
