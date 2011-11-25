using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListSnapshotPoliciesResponse : ICommandResponse
    {
        public IEnumerable<Account> SnapshotPolicies { get; set; }
    }
}