using System;
using Ninefold.API.Core;

namespace Ninefold.API.Compute.Messages
{
    public class MachineResponse  : ICommandResponse
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public int CpuNumber { get; set; }
        public double CpuSpeed { get; set; }
        public int CpuUsed { get; set; }
        public DateTime Created { get; set; }
        public string DisplayName { get; set; }       
        public string Domain { get; set; }
        public string DomainId { get; set; }
        public bool ForVirtualNetwork { get; set; }
        public string Group { get; set; }
        public string GroupId { get; set; }
        public string GuestOSId { get; set; }
        public bool HaEnable { get; set; }
        public string HostId { get; set; }
        public string HostName { get; set; }
        public string Hypervisor { get; set; }
        public string IPAddress { get; set; }
        public string IsoDisplayText { get; set; }
        public string IsoId { get; set; }
        public string JobId { get; set; }
        public string JobStatus { get; set; }
        public int Memory { get; set; }
        public string Name { get; set; }
        public long NetworkKBsRead { get; set; }
        public long NetworkKBsWrite { get; set; }
        public string Password { get; set; }
        public bool PasswordEnabled { get; set; }
        public string RootDeviceId { get; set; }
        public string RootDeviceType { get; set; }
        public string ServiceOfferingId { get; set; }
        public string ServiceOfferingName { get; set; }
        public string State { get; set; }
        public string TemplatedDisplayText { get; set; }
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public NIC[] NIC { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class NIC    
    {
        public string Id { get; set; }
        public string BroadcastUri { get; set; }
        public string Gateway { get; set; }
    }
}
