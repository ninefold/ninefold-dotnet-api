using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetSystemMetadataResponse : ICommandResponse
    {
        public string Metadata { get; set; }
        public string Policy { get; set; }
    }
}