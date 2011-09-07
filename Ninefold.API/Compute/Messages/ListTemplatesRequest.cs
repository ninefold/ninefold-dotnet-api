using System.ComponentModel.DataAnnotations;

namespace Ninefold.Compute.Messages
{
    public class ListTemplatesRequest : IComputeCommandRequest
    {
        public string Command { get { return "listTemplates"; } }

        [Required]
        public string TemplateFilter { get; set; }

        public string Account { get; set; }

        public string DomainId { get; set; }

        public string Hypervisor { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ZoneId { get; set; }
    }
}