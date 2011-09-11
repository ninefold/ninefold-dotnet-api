namespace Ninefold.Compute.Messages
{
    public class ListServiceOfferingsRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "listServiceOfferings"; }
        }

        public string DomainId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string VirtualMachineId { get; set; }
    }
}