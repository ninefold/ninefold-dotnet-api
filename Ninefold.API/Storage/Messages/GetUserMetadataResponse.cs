using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetUserMetadataResponse : ICommandResponse
    {
        public string Tags { get; set; }
        public string ListableTags { get; set; }
        public string Policy { get; set; }
    }
}