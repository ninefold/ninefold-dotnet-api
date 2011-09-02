using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class SetUserMetadataResponse : ICommandResponse
    {
        public string Policy { get; set; }
    }
}