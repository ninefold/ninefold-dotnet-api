using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;

namespace Ninefold.API.Storage
{
    public class StorageCommandExecutor : ICommandExecutor
    {
        public ICommandResponse Execute(ICommand command)
        {
            command.Prepare();
            return command.Execute();
        }
    }
}
