using System.ComponentModel.DataAnnotations;

namespace Ninefold.Compute.Messages
{
    public class ListTemplatesRequest : IComputeCommandRequest
    {
        public string Command { get { return "listTemplates"; } }

        [Required]
        public string TemplateFilter { get; set; }
    }
}