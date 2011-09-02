using System;
using Ninefold.Core;

namespace Ninefold.Compute
{
    public class ComputeClient : IComputeClient, ICommandExecutor
    {
        public IVirtualMachine VirtualMachine { get; private set; }
        
        public ComputeClient (string apiKey, string base64Secret)
            : this(apiKey, base64Secret, "https://api.ninefold.com/compute/v1.0/")
        { }

        public ComputeClient(string apiKey, string base64Secret, string computeServiceUrlRoot)
        {
            VirtualMachine = new VirtualMachine(apiKey, base64Secret, computeServiceUrlRoot);
        }

        ICommandResponse ICommandExecutor.Execute(ICommand command)            
        {
            throw new NotImplementedException("");
            //return command.ParseResponse();
        }
    }
}
