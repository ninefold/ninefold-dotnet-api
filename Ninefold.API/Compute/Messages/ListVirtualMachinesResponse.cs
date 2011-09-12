using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListVirtualMachinesResponse : ICommandResponse
    {
        public IEnumerable<Machine> Machines { get; set; }
    }
}