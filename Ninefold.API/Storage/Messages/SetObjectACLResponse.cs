using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class SetObjectACLResponse : ICommandResponse
    {
        public string ErrorMessage { get; set; }
        public string Policy { get; set; }
    }
}