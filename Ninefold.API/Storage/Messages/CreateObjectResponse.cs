using System.Net;

namespace Ninefold.API.Storage.Messages
{
    public class CreateObjectResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public string Location { get; set; }
        public string Delta { get; set; }
        public string Policy { get; set; }
    }
}