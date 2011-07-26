using Ninefold.API.Compute.Commands;
using Ninefold.API.Compute.Messages;

namespace Ninefold.API.Compute
{
    public interface IVirtualMachine
    {
        MachineResponse Deploy(DeployVirtualMachineRequest parameters);

        MachineResponse Start(StartVirtualMachineRequest parameters);

        MachineResponse Stop();
    }
}
