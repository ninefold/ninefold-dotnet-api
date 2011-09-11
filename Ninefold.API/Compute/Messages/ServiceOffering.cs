using System;
using System.Linq;
using System.Xml.Linq;

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
                           Id = int.Parse(ExtractValue("id", serviceOfferingElement)),
                           CPUNumber = int.Parse(ExtractValue("cpuNumber", serviceOfferingElement)),
                           CPUSpeed = int.Parse(ExtractValue("cpuSpeed", serviceOfferingElement)),
                           Created = string.IsNullOrWhiteSpace(ExtractValue("created", serviceOfferingElement)) ? 
                                                    DateTime.MinValue : DateTime.Parse(ExtractValue("created", serviceOfferingElement)),
                           DisplayText = ExtractValue("displayText", serviceOfferingElement),
                           Domain = ExtractValue("domain", serviceOfferingElement),
                           DomainId =  string.IsNullOrWhiteSpace(ExtractValue("domainId", serviceOfferingElement)) ? 0 : int.Parse(ExtractValue("domainId", serviceOfferingElement)),
                           Memory = int.Parse(ExtractValue("memory", serviceOfferingElement)),
                           Name = ExtractValue("name", serviceOfferingElement),
                           OffersHighAvailability = bool.Parse(ExtractValue("offerha", serviceOfferingElement)),
                           StorageType = ExtractValue("storageType", serviceOfferingElement),
                       };

            return offering;
        }

        private static string ExtractValue(string fieldName, XContainer document)
        {
            var node = document.Elements()
                .Where(e => e.Name.LocalName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));

            return node.Count() > 0 ? node.First().Value : string.Empty;
        } 
    }
}