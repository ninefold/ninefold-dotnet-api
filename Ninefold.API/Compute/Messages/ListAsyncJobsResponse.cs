using System.Collections.Generic;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute
{
    public class ListAsyncJobsResponse : ICommandResponse
    {
        public IEnumerable<Job> Jobs { get; set; }
    }
    
}
