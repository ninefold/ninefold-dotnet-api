using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetUserMetadataResponse : ICommandResponse
    {
        public string Tags { get; set; }
        public string ListableTags { get; set; }
        public string Policy { get; set; }
    }
}