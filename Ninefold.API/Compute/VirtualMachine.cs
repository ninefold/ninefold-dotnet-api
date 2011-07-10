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

        public VirtualMachine(string apiKey, string machineId, string computeServiceBaseUrl)
        {
            _apiKey = apiKey;
            _computeService = new ComputeService(computeServiceBaseUrl);
            _machineId = machineId;
        }

        public static VirtualMachine Start(string apiKey, string machineId, string computeServiceRootUrl, byte[] secret)
        {
            var virtualMachine = new VirtualMachine(apiKey, machineId, computeServiceRootUrl);
            virtualMachine.Start(secret);
            
            return virtualMachine;
        }

        public MachineResponse Start(byte[] secret)
        {
            var startCommand = new StartVirtualMachine(_computeService, secret);
            startCommand.ApiKey = _apiKey;
            startCommand.MachineId = _machineId;
            return startCommand.Execute();
        }

    }
}
