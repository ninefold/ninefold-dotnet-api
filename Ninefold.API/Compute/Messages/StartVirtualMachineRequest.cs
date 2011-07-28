using System;
using Ninefold.API.Core;

namespace Ninefold.API.Compute.Messages
{
    public class StartVirtualMachineRequest : ICommandRequest
    {
        public string MachineId { get; set; }

        public Uri Resource { get; set; }
    }
}