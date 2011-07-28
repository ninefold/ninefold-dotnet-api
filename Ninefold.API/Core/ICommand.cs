using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.API.Core
{
    public interface ICommand
    {
        ICommandResponse Execute();
        void Prepare();
    }
}
