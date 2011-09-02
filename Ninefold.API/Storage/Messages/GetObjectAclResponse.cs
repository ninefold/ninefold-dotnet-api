using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetObjectAclResponse : ICommandResponse
    {
        public string GroupAcl { get; set; }
        public string UserAcl { get; set; }
        public string Policy { get; set; }
    }
}