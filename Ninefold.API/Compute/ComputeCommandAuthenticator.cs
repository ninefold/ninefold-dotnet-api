using System;
using System.Linq;
using System.Text;

namespace Ninefold.Compute
{
    internal class ComputeCommandAuthenticator : IComputeCommandAuthenticator
    {
        public string AuthenticateRequest(string queryString, string base64Secret)
        {
            var queryBytes = Encoding.UTF8.GetBytes(queryString.ToLower());
            var secret = Encoding.UTF8.GetBytes(base64Secret);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);
            var hashedQuery = hashingAlg.ComputeHash(queryBytes);
            var base64Hash = Convert.ToBase64String(hashedQuery);
            var signature = Uri.EscapeDataString(base64Hash);

            return signature;
        }
    }
}
