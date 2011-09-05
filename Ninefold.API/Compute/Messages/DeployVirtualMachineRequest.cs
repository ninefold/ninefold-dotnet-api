using System.ComponentModel.DataAnnotations;
using Ninefold.Compute.Commands;

namespace Ninefold.Compute.Messages
{
    public class DeployVirtualMachineRequest : IComputeCommandRequest
    {
        public string Command { get { return "deployVirtualMachine"; } }

        [Required]
        public string ServiceOfferingId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public string ZoneId { get; set; }
    }
}