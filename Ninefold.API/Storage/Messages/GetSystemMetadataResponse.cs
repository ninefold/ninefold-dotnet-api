using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetSystemMetadataResponse : ICommandResponse
    {
        public string Metadata { get; set; }
        public string Policy { get; set; }
    }
}