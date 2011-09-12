using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class SecurityGroup
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public int DomainId { get; set; }
        public string Name { get; set; }

        internal static SecurityGroup From(XElement groupElement)
        {
            return new SecurityGroup
                       {
                           Id = int.Parse(groupElement.ExtractValue("id")),
                           Account = groupElement.ExtractValue("account"),
                           Description = groupElement.ExtractValue("description"),
                           Domain = groupElement.ExtractValue("domain"),
                           DomainId = int.Parse(groupElement.ExtractValue("domainId")),
                           Name = groupElement.ExtractValue("name"),
                       };
        }
    }
}
