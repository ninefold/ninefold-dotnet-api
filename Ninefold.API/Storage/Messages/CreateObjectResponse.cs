using System.Net;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class CreateObjectResponse : ICommandResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public string Location { get; set; }
        public string Delta { get; set; }
        public string Policy { get; set; }

        public string ErrorMessage { get; set; }
    }
}