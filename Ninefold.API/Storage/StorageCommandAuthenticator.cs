using System;
using System.Linq;
using System.Net;
using System.Text;
using Ninefold.Core;

namespace Ninefold.Storage
{
    public class StorageCommandAuthenticator : IStorageCommandAuthenticator
    {
        public void AuthenticateRequest(WebRequest request, string base64Secret)
        {
            var secret = Convert.FromBase64String(base64Secret);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);

            var canonHeaders = request.Headers.AllKeys
                .Where(h => h.StartsWith("x-emc", StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(h => h.ToLowerInvariant())
                .Select(h => h + ":" + request.Headers[h].Trim())
                .Aggregate(string.Empty, (current, header) => current + header + "\n")
                .TrimEnd('\n');

            var contentType = request.ContentType ?? string.Empty;
            var range = request.Headers["range"] ?? string.Empty;
            var date = request.Headers["date"] ?? string.Empty;
            var requestRelativeUri = Uri.UnescapeDataString(request.RequestUri.Segments.Aggregate(string.Empty, (current, segment) => current + segment) + request.RequestUri.Query);
            
            var hashString = request.Method + "\n" +
                             contentType + "\n" +
                             range + "\n" +
                             date + "\n" +
                             requestRelativeUri + "\n" +
                             canonHeaders;

            var signature = Convert.ToBase64String(hashingAlg.ComputeHash(Encoding.Default.GetBytes(hashString)));
            request.Headers.Add("x-emc-signature", signature);
        }
    }
}
