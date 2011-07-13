using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.API.Compute
{
    public interface IComputeClient
    {
        IVirtualMachine VirtualMachine { get; }
    }
}
