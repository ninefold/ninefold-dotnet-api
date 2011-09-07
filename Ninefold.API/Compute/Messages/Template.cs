using System;
using System.Linq;
using System.Xml.Linq;

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
                           Id = long.Parse(ExtractValue("id", templateElement)),
                           Name = ExtractValue("name", templateElement),
                           DisplayText = ExtractValue("displayText", templateElement),
                           Public = bool.Parse(ExtractValue("isPublic", templateElement)),
                           Created = DateTime.Parse(ExtractValue("created", templateElement)),
                           Ready = bool.Parse(ExtractValue("isReady", templateElement)),
                           PasswordEnabled = bool.Parse(ExtractValue("passwordEnabled", templateElement)),
                           Format = ExtractValue("format", templateElement),
                           Featured = bool.Parse(ExtractValue("isFeatured", templateElement)),
                           CrossZones = bool.Parse(ExtractValue("crossZones", templateElement)),
                           OSTypeId = int.Parse(ExtractValue("osTypeId", templateElement)),
                           OSType = ExtractValue("osTypeName", templateElement),
                           Account = ExtractValue("account", templateElement),
                           ZoneId = int.Parse(ExtractValue("zoneId", templateElement)),
                           Zone = ExtractValue("zoneName", templateElement),
                           Status = ExtractValue("status", templateElement),
                           Size = long.Parse(ExtractValue("size", templateElement)),
                           TemplateType = ExtractValue("templateType", templateElement),
                           Hypervisor = ExtractValue("hypervisor", templateElement),
                           Domain = ExtractValue("domain", templateElement),
                           DomainId = int.Parse(ExtractValue("domainId", templateElement)),
                           Extractable = bool.Parse(ExtractValue("isExtractable", templateElement))
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