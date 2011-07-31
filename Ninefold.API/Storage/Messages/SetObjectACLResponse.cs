using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class SetObjectACLResponse : ICommandResponse
    {
        public string ErrorMessage { get; set; }
        public string Policy { get; set; }
    }
}