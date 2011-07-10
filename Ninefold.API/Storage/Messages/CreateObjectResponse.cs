namespace Ninefold.API.Storage.Messages
{
    public class CreateObjectResponse
    {
        public string Location { get; set; }
        public string Delta { get; set; }
        public string Policy { get; set; }
    }
}