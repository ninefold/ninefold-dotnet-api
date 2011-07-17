using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ninefold.API.Compute;
using Ninefold.API.Storage;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var computeClient = new ComputeClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246", "FGJeXUzxCz5poHoSzRzmMTceuek=");
            var machineResponse = computeClient.VirtualMachine.Deploy();
            computeClient.VirtualMachine.Start(machineResponse.Id);

            
        }
    }
}
