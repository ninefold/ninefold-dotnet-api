using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListTemplatePermissionsResponse : ICommandResponse
    {
        public int Id { get; set; }
        public string Accounts { get; set; }
        public int DomainId { get; set; }
        public bool IsPublic { get; set; }
    }
}