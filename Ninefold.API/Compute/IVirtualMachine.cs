using Ninefold.API.Compute.Messages;

namespace Ninefold.API.Compute
{
    public interface IVirtualMachine
    {
        MachineResponse Deploy();

        MachineResponse Start(string machineId);

        MachineResponse Stop();
    }
}
