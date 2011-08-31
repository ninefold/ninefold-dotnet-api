using System.Xml.Linq;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class ListObjectsResponse : ICommandResponse
    {
        public string Policy { get; set; }
        public XDocument Content { get; set; }
        public string Token { get; set; }
    }
}
