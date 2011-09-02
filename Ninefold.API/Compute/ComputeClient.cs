using System;
using Ninefold.Core;

namespace Ninefold.Compute
{
    internal class ComputeClient : IComputeClient, ICommandExecutor
    {
        public ComputeClient (string apiKey, string base64Secret)
            : this(apiKey, base64Secret, "https://api.ninefold.com/compute/v1.0/")
        { }

        public ComputeClient(string apiKey, string base64Secret, string computeServiceUrlRoot)
        {
        }

        ICommandResponse ICommandExecutor.Execute(ICommand command)            
        {
            throw new NotImplementedException("");
            //return command.ParseResponse();
        }
    }
}
