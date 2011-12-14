using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.Compute.Messages
{
    public class ListAsyncJobsRequest : IComputeCommandRequest
    {
        public string Command { get { return "listAsyncJobs"; } }
        public string Account { get; set; }
        public string DomainId { get; set; }
        public string StartDate { get; set; }
    }
}
