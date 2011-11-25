using System.ComponentModel.DataAnnotations;

namespace Ninefold.Compute.Messages
{
    public class ListSnapshotPoliciesRequest : IComputeCommandRequest
    {
        public string Command { get { return "listSnapshotPolicies"; } }

        [Required]
        public string VolumeId { get; set; }
        public string Account { get; set; }
        public string DomainId { get; set; }
    }
}