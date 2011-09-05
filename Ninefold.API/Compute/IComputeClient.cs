using Ninefold.Compute.Commands;
using Ninefold.Compute.Messages;

namespace Ninefold.Compute
{
    public interface IComputeClient
    {
        MachineResponse DeployVirtualMachine(DeployVirtualMachineRequest request);
    }
}
