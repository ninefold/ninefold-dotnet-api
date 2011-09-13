namespace Ninefold.Compute.Messages
{
    public class ListIsosRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "listIsos"; }
        }

        public string Account { get; set; }
        public string Bootable { get; set; }
        public string DomainId { get; set; }
        public string Hypervisor { get; set; }
        public string Id { get; set; }
        public string IsoFilter { get; set; }
        public string IsPublic { get; set; }
        public string IsReady { get; set; }
        public string Name { get; set; }
        public string ZoneId { get; set; }
    }
}