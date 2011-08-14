using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class DeleteUserMetadataResponse : ICommandResponse
    {
        public string Policy { get; set; }
        public string Delta { get; set; }

        public string ErrorMessage { get; set; }
    }
}