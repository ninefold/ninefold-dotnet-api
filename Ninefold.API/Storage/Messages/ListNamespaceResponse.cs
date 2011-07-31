using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class ListNamespaceResponse : ICommandResponse
    {
        public string UserAcl { get; set; }
        public string GroupAcl { get; set; }
        public byte[] Content { get; set; }
        public string Meta { get; set; }
        public string Policy { get; set; }
        public string ErrorMessage { get; set; }
    }
}
