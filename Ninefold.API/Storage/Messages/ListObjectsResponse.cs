using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class ListObjectsResponse : ICommandResponse
    {
        public string Policy { get; set; }
        public XDocument Content { get; set; }
        public string Token { get; set; }
    }
}
