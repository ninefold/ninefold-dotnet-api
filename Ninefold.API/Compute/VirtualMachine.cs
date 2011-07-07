using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.API.Compute
{
    public class VirtualMachine
    {
        readonly string _apiKey;
        readonly string _machineId;

        public VirtualMachine(string apiKey, string machineId)
        {
            _apiKey = apiKey;
            _machineId = machineId;
        }

        public static VirtualMachine Start(string apiKey, string machineId)
        {
            var virtualMachine = new VirtualMachine(apiKey, machineId);
            return virtualMachine;
        }

    }
}
