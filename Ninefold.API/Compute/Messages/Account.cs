using System;
using System.Linq;
using System.Xml.Linq;

namespace Ninefold.Compute.Messages
{
    public class Account
    {
        public User User { get; set; }
        public string State { get; set; }
        public int VMRunning { get; set; }
        public int VMStopped { get; set; }
        public int TemplateAvailable { get; set; }
        public int TemplateTotal { get; set; }
        public int TemplateLimit { get; set; }
        public int SnapshotAvailable { get; set; }
        public int SnapshotTotal { get; set; }
        public int SnapshotLimit { get; set; }
        public int VolumeAvailable { get; set; }
        public int VolumeTotal { get; set; }
        public int VolumeLimit { get; set; }
        public int IPAvailable { get; set; }
        public int IPTotal { get; set; }
        public int IPLimit { get; set; }
        public int VMAvailable { get; set; }
        public int VMTotal { get; set; }
        public int VMLimit { get; set; }
        public string SentBytes { get; set; }
        public string ReceivedBytes { get; set; }
        public string Domain { get; set; }
        public int DomainId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        internal static Account From(XElement accountElement)
        {
            return new Account
                       {
                           Id = int.Parse(ExtractValue("id", accountElement)),
                           Name = ExtractValue("name", accountElement),
                           Type = ExtractValue("accountType", accountElement),
                           DomainId = int.Parse(ExtractValue("domainId", accountElement)),
                           Domain = ExtractValue("domain", accountElement),
                           ReceivedBytes = ExtractValue("receivedBytes", accountElement),
                           SentBytes = ExtractValue("sentBytes", accountElement),
                           VMLimit = int.Parse(ExtractValue("vmLimit", accountElement)),
                           VMTotal = int.Parse(ExtractValue("vmTotal", accountElement)),
                           VMAvailable = int.Parse(ExtractValue("vmAvailable", accountElement)),
                           IPLimit = int.Parse(ExtractValue("ipLimit", accountElement)),
                           IPTotal = int.Parse(ExtractValue("ipTotal", accountElement)),
                           IPAvailable = int.Parse(ExtractValue("ipAvailable", accountElement)),
                           VolumeLimit = int.Parse(ExtractValue("volumeLimit", accountElement)),
                           VolumeTotal = int.Parse(ExtractValue("volumeTotal", accountElement)),
                           VolumeAvailable = int.Parse(ExtractValue("volumeAvailable", accountElement)),
                           SnapshotLimit = int.Parse(ExtractValue("snapshotLimit", accountElement)),
                           SnapshotTotal = int.Parse(ExtractValue("snapshotTotal", accountElement)),
                           SnapshotAvailable = int.Parse(ExtractValue("snapshotAvailable", accountElement)),
                           TemplateLimit = int.Parse(ExtractValue("templateLimit", accountElement)),
                           TemplateTotal = int.Parse(ExtractValue("templateTotal", accountElement)),
                           TemplateAvailable = int.Parse(ExtractValue("templateAvailable", accountElement)),
                           VMStopped = int.Parse(ExtractValue("vmStopped", accountElement)),
                           VMRunning = int.Parse(ExtractValue("vmRunning", accountElement)),
                           State = ExtractValue("state", accountElement),
                           User = User.From(accountElement.Elements().First(e => e.Name.LocalName.Equals("user")))
                       };
        }

        private static string ExtractValue(string fieldName, XContainer document)
        {
            return document.Elements()
                                .First(e => e.Name.LocalName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                                .Value;
        }    
    }
}