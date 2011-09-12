using System.ComponentModel.DataAnnotations;

namespace Ninefold.Compute.Messages
{
    public class ListTemplatePermissionsRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "listTemplatePermissions"; }
        }

        [Required]
        public string Id { get; set; }
        public string Account { get; set; }
        public string DomainId { get; set; }
    }
}