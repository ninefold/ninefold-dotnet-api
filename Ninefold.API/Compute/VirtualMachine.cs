using System.Collections.Generic;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;

namespace Ninefold.API.Compute
{
    public class VirtualMachine
    {
        readonly string _apiKey;
        readonly string _machineId;
        readonly INinefoldService _computeService;

        public VirtualMachine(string apiKey,  string computeServiceBaseUrl)
        {
            _apiKey = apiKey;
            _computeService = new ComputeService(computeServiceBaseUrl);
        }

        public static VirtualMachine Deploy(string apiKey, string computeServiceRootUrl, byte[] secret, string serviceOfferingId, string templateId, string zoneId, IDictionary<string, string> additionalValues = null)
        {
            var virtualMachine = new VirtualMachine(apiKey, computeServiceRootUrl);
            virtualMachine.Deploy(secret);
            return virtualMachine;
        }

        public static VirtualMachine Start(string apiKey, string machineId, string computeServiceRootUrl, byte[] secret)
        {
            var virtualMachine = new VirtualMachine(apiKey, computeServiceRootUrl);
            virtualMachine.Start(secret, machineId);            
            return virtualMachine;
        }

        public MachineResponse Deploy(byte[] secret)
        {
            var deployCommand = new DeployVirtualMachine(_apiKey, secret, _computeService);
            return deployCommand.Execute();
        }

        public MachineResponse Start(byte[] secret, string machineId)
        {
            var startCommand = new StartVirtualMachine(_computeService, secret, _apiKey, machineId);
            return startCommand.Execute();
        }

        
    }
}
