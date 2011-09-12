using System;
using System.Linq;
using System.Xml.Linq;
using Ninefold.Core;

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
                           Id = int.Parse(accountElement.ExtractValue("id")),
                           Name = accountElement.ExtractValue("name"),
                           Type = accountElement.ExtractValue("accountType"),
                           DomainId = int.Parse(accountElement.ExtractValue("domainId")),
                           Domain = accountElement.ExtractValue("domain"),
                           ReceivedBytes = accountElement.ExtractValue("receivedBytes"),
                           SentBytes = accountElement.ExtractValue("sentBytes"),
                           VMLimit = int.Parse(accountElement.ExtractValue("vmLimit")),
                           VMTotal = int.Parse(accountElement.ExtractValue("vmTotal")),
                           VMAvailable = int.Parse(accountElement.ExtractValue("vmAvailable")),
                           IPLimit = int.Parse(accountElement.ExtractValue("ipLimit")),
                           IPTotal = int.Parse(accountElement.ExtractValue("ipTotal")),
                           IPAvailable = int.Parse(accountElement.ExtractValue("ipAvailable")),
                           VolumeLimit = int.Parse(accountElement.ExtractValue("volumeLimit")),
                           VolumeTotal = int.Parse(accountElement.ExtractValue("volumeTotal")),
                           VolumeAvailable = int.Parse(accountElement.ExtractValue("volumeAvailable")),
                           SnapshotLimit = int.Parse(accountElement.ExtractValue("snapshotLimit")),
                           SnapshotTotal = int.Parse(accountElement.ExtractValue("snapshotTotal")),
                           SnapshotAvailable = int.Parse(accountElement.ExtractValue("snapshotAvailable")),
                           TemplateLimit = int.Parse(accountElement.ExtractValue("templateLimit")),
                           TemplateTotal = int.Parse(accountElement.ExtractValue("templateTotal")),
                           TemplateAvailable = int.Parse(accountElement.ExtractValue("templateAvailable")),
                           VMStopped = int.Parse(accountElement.ExtractValue("vmStopped")),
                           VMRunning = int.Parse(accountElement.ExtractValue("vmRunning")),
                           State = accountElement.ExtractValue("state"),
                           User = User.From(accountElement.Elements().First(e => e.Name.LocalName.Equals("user")))
                       };
        }
    }
}