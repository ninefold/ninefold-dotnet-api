using System;
using System.Net;
using System.Text;
using Ninefold.API.Core;

namespace Ninefold.API.Compute
{
    public class ComputeRequestAuthenticator : ICommandAuthenticator
    {
        public void AuthenticateRequest(WebRequest request, string base64Secret)
        {
            var secret = Convert.FromBase64String(base64Secret);
            //var url = Encoding.Default.GetBytes(request.RequestUri.ToString());
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);
            Convert.ToBase64String(hashingAlg.ComputeHash(Encoding.UTF8.GetBytes(request.RequestUri.ToString())));           
            
        }
    }
}
