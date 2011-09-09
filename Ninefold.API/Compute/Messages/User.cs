using System;
using System.Linq;
using System.Xml.Linq;

namespace Ninefold.Compute.Messages
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string State { get; set; }
        public string Account { get; set; }
        public int AccountType { get; set; }
        public int DomainId { get; set; }
        public string Domain { get; set; }
        public string APIKey { get; set; }
        public string SecretKey { get; set; }

        internal static User From(XElement userElement)
        {
            return new User
                       {
                           Id = int.Parse(ExtractValue("id", userElement)),
                           Username = ExtractValue("username", userElement),
                           FirstName = ExtractValue("firstName", userElement),
                           LastName = ExtractValue("lastName", userElement),
                           Email = ExtractValue("email", userElement),
                           Created = DateTime.Parse(ExtractValue("created", userElement)),
                           State = ExtractValue("state", userElement),
                           Account = ExtractValue("account", userElement),
                           AccountType = int.Parse(ExtractValue("accountType", userElement)),
                           DomainId = int.Parse(ExtractValue("domainId", userElement)),
                           Domain = ExtractValue("domain", userElement),
                           APIKey = ExtractValue("apiKey", userElement),
                           SecretKey = ExtractValue("secretKey", userElement),
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