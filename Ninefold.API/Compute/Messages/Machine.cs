using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPUUsed { get; set; }
        public int CPUNumber { get; set; }
        public string Account { get; set; }
        public DateTime Created { get; set; }
        public string DisplayName { get; set; }
        public string Domain { get; set; }
        public int DomainId { get; set; }
        public string ForVirtualNetwork { get; set; }
        public string Group { get; set; }
        public int GroupId { get; set; }
        public int GuestOSId { get; set; }
        public bool HighAvailabilityEnabled { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }
        public string Hypervisor { get; set; }
        public string IPAddress { get; set; }
        public string ISODisplayText { get; set; }
        public int ISOId { get; set; }
        public string ISOName { get; set; }
        public string JobId { get; set; }
        public string JobStatus { get; set; }
        public int Memory { get; set; }
        public string NetworkKbsRead { get; set; }
        public string NetworkKbsWrite { get; set; }
        public string Password { get; set; }
        public bool PasswordEnabled { get; set; }
        public int RootDeviceId { get; set; }
        public string RootDeviceType { get; set; }
        public int ServiceOfferingId { get; set; }
        public string ServiceOfferingName { get; set; }
        public string State { get; set; }
        public string TemplateDisplayText { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public IEnumerable<NetworkInterface> AttachedNetworkInterfaces { get; set; }
        public IEnumerable<SecurityGroup> SecurityGroups { get; set; }
        public IEnumerable<IngressRule> IngressRules { get; set; }
        
        public static Machine From(XElement machineElement)
        {
            var machine =  new Machine
                               {
                                   Id = int.Parse(machineElement.ExtractValue("id")),
                                   Account = machineElement.ExtractValue("account"),
                                   CPUNumber = string.IsNullOrWhiteSpace(machineElement.ExtractValue("cpuNumber")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("cpuNumber")),
                                   CPUUsed = machineElement.ExtractValue("cpuUsed"),
                                   Created = DateTime.Parse(machineElement.ExtractValue("created")),
                                   DisplayName = machineElement.ExtractValue("displayName"),
                                   Domain = machineElement.ExtractValue("domain"),
                                   DomainId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("domainId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("domainId")),
                                   ForVirtualNetwork = machineElement.ExtractValue("forVirtualNetwork"),
                                   Group = machineElement.ExtractValue("group"),
                                   GroupId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("groupId")) ? 
                                                                0 : int.Parse(machineElement.ExtractValue("groupId")),
                                   GuestOSId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("guestOsId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("guestOsId")),
                                   HighAvailabilityEnabled = string.IsNullOrWhiteSpace(machineElement.ExtractValue("haEnable")) ?
                                                                false : bool.Parse(machineElement.ExtractValue("haEnable")),
                                   HostId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("hostId")) ? 
                                                                0 :  int.Parse(machineElement.ExtractValue("hostId")),
                                   HostName = machineElement.ExtractValue("hostname"),
                                   Hypervisor = machineElement.ExtractValue("hypervisor"),
                                   IPAddress = machineElement.ExtractValue("ipAddress"),
                                   ISODisplayText = machineElement.ExtractValue("isoDisplayText"),
                                   ISOId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("isoId")) ? 
                                                                0 : int.Parse(machineElement.ExtractValue("isoId")),
                                   ISOName = machineElement.ExtractValue("isoName"),
                                   JobId = machineElement.ExtractValue("jobId"),
                                   JobStatus = machineElement.ExtractValue("jobStatus"),
                                   Memory = string.IsNullOrWhiteSpace(machineElement.ExtractValue("memory")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("memory")),
                                   Name = machineElement.ExtractValue("name"),
                                   NetworkKbsRead = machineElement.ExtractValue("networkKbsRead"),
                                   NetworkKbsWrite = machineElement.ExtractValue("networkKbsWrite"),
                                   Password = machineElement.ExtractValue("password"),
                                   PasswordEnabled = string.IsNullOrWhiteSpace(machineElement.ExtractValue("passwordEnabled")) ?
                                                                false : bool.Parse(machineElement.ExtractValue("passwordEnabled")),
                                   RootDeviceId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("rootDeviceId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("rootDeviceId")),
                                   RootDeviceType = machineElement.ExtractValue("rootDeviceType"),
                                   ServiceOfferingId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("serviceOfferingId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("serviceOfferingId")),
                                   ServiceOfferingName = machineElement.ExtractValue("serviceOfferingName"),
                                   State = machineElement.ExtractValue("state"),
                                   TemplateDisplayText = machineElement.ExtractValue("templateDisplayText"),
                                   TemplateId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("templateId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("templateId")),
                                   TemplateName = machineElement.ExtractValue("templateName"),
                                   ZoneId = string.IsNullOrWhiteSpace(machineElement.ExtractValue("zoneId")) ?
                                                                0 : int.Parse(machineElement.ExtractValue("zoneId")),
                                   ZoneName = machineElement.ExtractValue("zoneName"),
                                   AttachedNetworkInterfaces = machineElement.Elements()
                                       .Where(
                                           e =>
                                           e.Name.LocalName.Equals("nic",
                                                                   StringComparison.InvariantCultureIgnoreCase))
                                       .Select(NetworkInterface.From),
                                   SecurityGroups = machineElement.Elements()
                                       .Where(
                                           e =>
                                           e.Name.LocalName.Equals("securityGroup",
                                                                   StringComparison.InvariantCultureIgnoreCase))
                                       .Select(SecurityGroup.From),
                                   IngressRules = machineElement.Elements()
                                       .Where(
                                           e =>
                                           e.Name.LocalName.Equals("ingressRule",
                                                                   StringComparison.InvariantCultureIgnoreCase))
                                       .Select(IngressRule.From),
                               };

            return machine;
        }        
    }
}