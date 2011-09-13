using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListVolumesResponse : ICommandResponse
    {
        public IEnumerable<Machine> Volumes { get; set; }
    }
}