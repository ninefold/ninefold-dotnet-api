using System;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class SnapshotPolicy
    {
        public int Id { get; set; }
        public int VolumeId { get; set; }
        public TimeSpan Schedule { get; set; }
        public int IntervalType { get; set; }
        public int MaxSnapshots { get; set; }
        public string TimeZone { get; set; }

        public static SnapshotPolicy From(XElement policyElement)
        {
            return new SnapshotPolicy
                       {
                           Id = int.Parse(policyElement.ExtractValue("id")),
                           VolumeId = int.Parse(policyElement.ExtractValue("volumeId")),
                           Schedule = TimeSpan.Parse(policyElement.ExtractValue("schedule")),
                           IntervalType = int.Parse(policyElement.ExtractValue("intervalType")),
                           MaxSnapshots = int.Parse(policyElement.ExtractValue("maxSnaps")),
                           TimeZone = policyElement.ExtractValue("timeZone")
                       };
        }
    }
}