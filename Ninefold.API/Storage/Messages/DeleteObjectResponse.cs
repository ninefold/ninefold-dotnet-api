using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class DeleteObjectResponse : ICommandResponse
    {
        public long Delta { get; set; }
        public string Policy { get; set; }
        public string ErrorMessage { get; set; }
    }
}