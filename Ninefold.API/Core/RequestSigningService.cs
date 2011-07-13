using System;
using System.Text;

namespace Ninefold.API.Core
{
    public class RequestSigningService : IRequestSigningService
    {
        public string GenerateRequestSignature(Uri uri, string base64Secret)
        {
            var secret = Convert.FromBase64String(base64Secret);
            var url = Encoding.UTF8.GetBytes(uri.ToString());
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);
            
            return hashingAlg.ComputeHash(url).ToString();           
        }
    }
}
