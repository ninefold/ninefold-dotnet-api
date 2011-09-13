using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListIsosResponse : ICommandResponse
    {
        public IEnumerable<Machine> Isos { get; set; }
    }
}