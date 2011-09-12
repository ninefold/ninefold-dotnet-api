using System;
using System.Linq;
using System.Xml.Linq;
using Ninefold.Core;

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
                           Id = int.Parse(userElement.ExtractValue("id")),
                           Username = userElement.ExtractValue("username"),
                           FirstName = userElement.ExtractValue("firstName"),
                           LastName = userElement.ExtractValue("lastName"),
                           Email = userElement.ExtractValue("email"),
                           Created = DateTime.Parse(userElement.ExtractValue("created")),
                           State = userElement.ExtractValue("state"),
                           Account = userElement.ExtractValue("account"),
                           AccountType = int.Parse(userElement.ExtractValue("accountType")),
                           DomainId = int.Parse(userElement.ExtractValue("domainId")),
                           Domain = userElement.ExtractValue("domain"),
                           APIKey = userElement.ExtractValue("apiKey"),
                           SecretKey = userElement.ExtractValue("secretKey"),
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