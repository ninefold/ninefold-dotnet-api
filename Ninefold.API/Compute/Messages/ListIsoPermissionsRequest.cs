using System.ComponentModel.DataAnnotations;

namespace Ninefold.Compute.Messages
{
    public class ListIsoPermissionsRequest : IComputeCommandRequest
    {
        public string Command { get { return "listIsoPermissions"; } }
        
        [Required]
        public string Id { get; set; }
        public string Account { get; set; }
        public string DomainId { get; set; }
    }
}