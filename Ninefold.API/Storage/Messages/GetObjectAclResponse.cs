using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetObjectAclResponse : ICommandResponse
    {
        public string GroupAcl { get; set; }
        public string UserAcl { get; set; }
        public string Policy { get; set; }
    }
}