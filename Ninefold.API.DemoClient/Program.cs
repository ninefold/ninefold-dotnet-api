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
            var computeClient = new ComputeClient("12345", Convert.ToBase64String(new byte[] { 0x1, 0x2 }));

            var machineResponse = computeClient.VirtualMachine.Deploy();
            computeClient.VirtualMachine.Start(machineResponse.Id);
        }
    }
}
