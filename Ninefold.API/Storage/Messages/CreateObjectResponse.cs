using System.Net;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class CreateObjectResponse : ICommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public string Location { get; set; }
        public string Delta { get; set; }
        public string Policy { get; set; }
    }
}