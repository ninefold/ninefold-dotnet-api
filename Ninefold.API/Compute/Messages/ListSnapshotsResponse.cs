using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListSnapshotsResponse : ICommandResponse
    {
        public IEnumerable<Snapshot> Snapshots { get; set; }
    }
}
