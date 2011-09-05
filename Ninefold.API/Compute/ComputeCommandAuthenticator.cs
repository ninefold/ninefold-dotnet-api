using System;
using System.Linq;
using System.Text;

namespace Ninefold.Compute
{
    internal class ComputeCommandAuthenticator : IComputeCommandAuthenticator
    {
        public string AuthenticateRequest(string queryString, string base64Secret)
        {
            var secret = Convert.FromBase64String(base64Secret);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);

            var escapedQueryBytes = Encoding.UTF8.GetBytes(queryString);
            var hashedQuery = hashingAlg.ComputeHash(escapedQueryBytes);
            var base64Hash = Convert.ToBase64String(hashedQuery);
            var signature = Uri.EscapeUriString(base64Hash).Replace("+", "%20");

            return string.Format("{0}&signature={1}", queryString, signature);
        }
    }
}
