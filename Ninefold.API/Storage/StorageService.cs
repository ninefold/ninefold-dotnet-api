using Ninefold.API.Compute.Commands;
using Ninefold.API.Core;

namespace Ninefold.API.Storage
{
    public class StorageService : ICommandExecutor
    {        
        public ICommandResponse Execute(ICommand command)
        {
            return command.Execute();
        }
    }
}
