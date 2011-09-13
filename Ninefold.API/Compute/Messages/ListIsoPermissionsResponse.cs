using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListIsoPermissionsResponse : ICommandResponse
    {
        public int Id { get; set; }
        public string Accounts { get; set; }
        public int DomainId { get; set; }
        public bool IsPublic { get; set; }
    }
}