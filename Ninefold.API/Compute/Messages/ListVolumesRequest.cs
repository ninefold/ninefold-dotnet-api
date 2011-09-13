namespace Ninefold.Compute.Messages
{
    public class ListVolumesRequest : IComputeCommandRequest
    {
        public string Command { get { return  "listVolumes"; } }
        public string Account { get; set; }
        public string DomainId { get; set; }
        public string HostId { get; set; }
        public string Id { get; set; }
        public string IsRecursive { get; set; }
        public string Name { get; set; }
        public string PodId { get; set; }
        public string Type { get; set; }
        public string VirtualMachineId { get; set; }
        public string ZoneId { get; set; }
    }
}