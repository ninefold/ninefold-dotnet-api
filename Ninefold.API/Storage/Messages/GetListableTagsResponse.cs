using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetListableTagsResponse : ICommandResponse
    {       
        public string Policy { get; set; }
        public string Tags { get; set; }
        public string Token { get; set; }
    }
}