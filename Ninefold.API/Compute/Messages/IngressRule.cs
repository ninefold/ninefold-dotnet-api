using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class IngressRule
    {
        public string Account { get; set; }
        public string CIDR { get; set; }
        public int EndPort { get; set; }
        public string ICMPCode { get; set; }
        public string ICMPType { get; set; }
        public string Protocol { get; set; }
        public int Id { get; set; }
        public string SecurityGroupName { get; set; }
        public int StartPort { get; set; }

        internal static IngressRule From(XElement ruleElement)
        {
            return new IngressRule
                       {
                           Account = ruleElement.ExtractValue("account"),
                           CIDR = ruleElement.ExtractValue("cidr"),
                           EndPort = int.Parse(ruleElement.ExtractValue("endPort")),
                           ICMPCode = ruleElement.ExtractValue("icmpCode"),
                           ICMPType = ruleElement.ExtractValue("icmpType"),
                           Protocol = ruleElement.ExtractValue("protocol"),
                           Id = int.Parse(ruleElement.ExtractValue("ruleId")),
                           SecurityGroupName = ruleElement.ExtractValue("securityGroupName"),
                           StartPort = int.Parse(ruleElement.ExtractValue("startPort")),
                       };
        }

    }
}
