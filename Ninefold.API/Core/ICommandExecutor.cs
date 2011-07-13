using System;
using System.Security;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Compute.Messages;
using RestSharp;

namespace Ninefold.API.Core
{
    public interface ICommandExecutor
    {
        ICommandResponse Execute(ICommand command);
    }
}