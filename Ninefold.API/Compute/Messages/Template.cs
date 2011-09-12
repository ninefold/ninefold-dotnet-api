using System;
using System.Linq;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class Template
    {
        public long Id { get; set; }
        public bool Extractable { get; set; }
        public int DomainId { get; set; }
        public string Domain { get; set; }
        public string Hypervisor { get; set; }
        public string TemplateType { get; set; }
        public long Size { get; set; }
        public string Status { get; set; }
        public string Zone { get; set; }
        public int ZoneId { get; set; }
        public string Account { get; set; }
        public string OSType { get; set; }
        public int OSTypeId { get; set; }
        public bool CrossZones { get; set; }
        public bool Featured { get; set; }
        public string Format { get; set; }
        public bool PasswordEnabled { get; set; }
        public bool Ready { get; set; }
        public DateTime Created { get; set; }
        public bool Public { get; set; }
        public string DisplayText { get; set; }
        public string Name { get; set; }

        internal static Template From(XElement templateElement)
        {
            return new Template
                       {
                           Id = long.Parse(templateElement.ExtractValue("id")),
                           Name = templateElement.ExtractValue("name"),
                           DisplayText = templateElement.ExtractValue("displayText"),
                           Public = bool.Parse(templateElement.ExtractValue("isPublic")),
                           Created = DateTime.Parse(templateElement.ExtractValue("created")),
                           Ready = bool.Parse(templateElement.ExtractValue("isReady")),
                           PasswordEnabled = bool.Parse(templateElement.ExtractValue("passwordEnabled")),
                           Format = templateElement.ExtractValue("format"),
                           Featured = bool.Parse(templateElement.ExtractValue("isFeatured")),
                           CrossZones = bool.Parse(templateElement.ExtractValue("crossZones")),
                           OSTypeId = int.Parse(templateElement.ExtractValue("osTypeId")),
                           OSType = templateElement.ExtractValue("osTypeName"),
                           Account = templateElement.ExtractValue("account"),
                           ZoneId = int.Parse(templateElement.ExtractValue("zoneId")),
                           Zone = templateElement.ExtractValue("zoneName"),
                           Status = templateElement.ExtractValue("status"),
                           Size = long.Parse(templateElement.ExtractValue("size")),
                           TemplateType = templateElement.ExtractValue("templateType"),
                           Hypervisor = templateElement.ExtractValue("hypervisor"),
                           Domain = templateElement.ExtractValue("domain"),
                           DomainId = int.Parse(templateElement.ExtractValue("domainId")),
                           Extractable = bool.Parse(templateElement.ExtractValue("isExtractable"))
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