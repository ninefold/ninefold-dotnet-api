using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class DeleteObjectResponse : ICommandResponse
    {
        public long Delta { get; set; }
        public string Policy { get; set; }
        public string ErrorMessage { get; set; }
    }
}