namespace Ninefold.Compute.Messages
{
    public class ListEventsRequest : IComputeCommandRequest
    {
        public string Command { get { return "listEvents"; } }
        public string Account { get; set; }
        public string DomainId { get; set; }
        public string Duration { get; set; }
        public string EndDate { get; set; }
        public string EntryTime { get; set; }
        public string Level { get; set; }
        public string StartDate { get; set; }
        public string Type { get; set; }
    }
}
