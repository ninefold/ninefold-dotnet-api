using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ninefold.API.Core;

namespace Ninefold.API.Compute.Messages
{
    public class DeployVirtualMachineRequest : ICommandRequest
    {
        [Required]
        public string ServiceOfferingId { get; set; }
        [Required]
        public string TemplateId { get; set; }
        [Required]
        public string ZoneId { get; set; }
        
        public string Account { get; set; }
        public string DiskOfferingId { get; set; }
        public string DisplayName { get; set; }
        public string DomainId { get; set; }
        public string Group { get; set; }
        public string Hypervisor { get; set; }
        public string KeyPair { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> NetworkIds { get; set; }
        public string SecurityGroupIds { get; set; }
        public string Size { get; set; }
        [StringLength(2048)]
        public string Base64UserData { get; set; }
 
    }
}
