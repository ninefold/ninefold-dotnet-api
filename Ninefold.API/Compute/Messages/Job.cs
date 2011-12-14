using System;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class Job
    {
        public string AccountId { get; set; }
        public string Command { get; set; }
        public DateTime Created { get; set; }
        public string JobId { get; set; }
        public string InstanceId { get; set; }
        public string InstanceType { get; set; }
        public string Progress { get; set; }
        public string Result { get; set; }
        public string ResultCode { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }

        internal static Job From(XElement jobElement)
        {
            return new Job
            {
                AccountId = jobElement.ExtractValue("accountId"),
                Command = jobElement.ExtractValue("cmd"),
                Created = DateTime.Parse(jobElement.ExtractValue("created")),
                JobId = jobElement.ExtractValue("jobId"),
                InstanceId = jobElement.ExtractValue("jobInstanceId"),
                InstanceType = jobElement.ExtractValue("jobInstanceType"),
                Progress = jobElement.ExtractValue("jobProcStatus"),
                Result = jobElement.ExtractValue("jobResult"),
                ResultCode = jobElement.ExtractValue("jobResultCode"),
                Status = jobElement.ExtractValue("jobStatus"),
                UserId = jobElement.ExtractValue("userId"),
            };
        }
    }
}
