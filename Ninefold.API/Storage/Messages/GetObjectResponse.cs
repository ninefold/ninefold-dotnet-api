using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetObjectResponse : ICommandResponse
    {
        public string UserAcl { get; set; }
        public string GroupAcl { get; set; }
        public byte[] Content { get; set; }
        public string Metadata { get; set; }
        public string ListableTags { get; set; }
        public string Policy { get; set; }
        public string ErrorMessage { get; set; }
    }
}