using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute.Messages;

namespace Ninefold.API
{
    public class NinefoldComputeClient
    {
        readonly string _apiKey;
        readonly byte[] _secret;

        public IVirtualMachine VirtualMachine { get; private set; }
        
        public NinefoldComputeClient (IVirtualMachine virtualMachine, string apiKey, byte[] secret)
        {
            VirtualMachine = virtualMachine;
            _apiKey = apiKey;
            _secret = secret;
        }
    }

    public interface IVirtualMachine
    {
        MachineResponse Deploy();

        MachineResponse Start(int machineId);

        MachineResponse Stop();
    }
}
