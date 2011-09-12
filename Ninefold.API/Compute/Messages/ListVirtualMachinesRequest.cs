namespace Ninefold.Compute.Messages
{
    public class ListVirtualMachinesRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "listVirtualMachines"; }
        }

        public string Account { get; set; }
        public string DomainId { get; set; }
        public string ForVirtualNetwork { get; set; }
        public string GroupId { get; set; }
        public string HostId { get; set; }
        public string Hypervisor { get; set; }
        public string Id { get; set; }
        public string IsRecursive { get; set; }
        public string Name { get; set; }
        public string NetworkId { get; set; }
        public string PodId { get; set; }
        public string State { get; set; }
        public string ZoneId { get; set; }
    }
}