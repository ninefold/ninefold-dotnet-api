using System;
using System.Linq;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ServiceOffering
    {
        public int Id { get; set; }
        public int CPUNumber { get; set; }
        public int CPUSpeed { get; set; }
        public DateTime Created { get; set; }
        public string DisplayText { get; set; }
        public string Domain { get; set; }
        public int DomainId { get; set; }
        public int Memory { get; set; }
        public string Name { get; set; }
        public bool OffersHighAvailability { get; set; }
        public string StorageType { get; set; }

        internal static ServiceOffering From(XElement serviceOfferingElement)
        {
            var offering =  new ServiceOffering
                       {
                           Id = int.Parse(serviceOfferingElement.ExtractValue("id")),
                           CPUNumber = int.Parse(serviceOfferingElement.ExtractValue("cpuNumber")),
                           CPUSpeed = int.Parse(serviceOfferingElement.ExtractValue("cpuSpeed")),
                           Created = string.IsNullOrWhiteSpace(serviceOfferingElement.ExtractValue("created")) ?
                                                    DateTime.MinValue : DateTime.Parse(serviceOfferingElement.ExtractValue("created")),
                           DisplayText = serviceOfferingElement.ExtractValue("displayText"),
                           Domain = serviceOfferingElement.ExtractValue("domain"),
                           DomainId = string.IsNullOrWhiteSpace(serviceOfferingElement.ExtractValue("domainId")) ? 0 : int.Parse(serviceOfferingElement.ExtractValue("domainId")),
                           Memory = int.Parse(serviceOfferingElement.ExtractValue("memory")),
                           Name = serviceOfferingElement.ExtractValue("name"),
                           OffersHighAvailability = bool.Parse(serviceOfferingElement.ExtractValue("offerha")),
                           StorageType = serviceOfferingElement.ExtractValue("storageType"),
                       };

            return offering;
        }
    }
}