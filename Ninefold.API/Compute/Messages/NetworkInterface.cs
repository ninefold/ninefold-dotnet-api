using System;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class NetworkInterface
    {
        public int Id { get; set; }
        public Uri BroadcastUri { get; set; }
        public string Gateway { get; set; }
        public string IPAddress { get; set; }
        public bool IsDefault { get; set; }
        public Uri IsolationUri { get; set; }
        public string Netmask { get; set; }
        public int NetworkId { get; set; }
        public string TrafficType { get; set; }
        public string Type { get; set; }
        
        internal static NetworkInterface From(XElement interfaceElement)
        {
            return new NetworkInterface
                       {
                           Id = int.Parse(interfaceElement.ExtractValue("id")),
                           BroadcastUri = new Uri(interfaceElement.ExtractValue("broadcastUri")),
                           Gateway = interfaceElement.ExtractValue("gateway"),
                           IPAddress = interfaceElement.ExtractValue("ipAddress"),
                           IsDefault = bool.Parse(interfaceElement.ExtractValue("isDefault")),
                           IsolationUri = new Uri(interfaceElement.ExtractValue("isolationUri")),
                           Netmask = interfaceElement.ExtractValue("netmask"),
                           NetworkId = int.Parse(interfaceElement.ExtractValue("networkId")),
                           TrafficType = interfaceElement.ExtractValue("trafficType"),
                           Type = interfaceElement.ExtractValue("type"),
                       };
        }

    }
}
