using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class SetUserMetadataResponse : ICommandResponse
    {
        public string Policy { get; set; }
    }
}