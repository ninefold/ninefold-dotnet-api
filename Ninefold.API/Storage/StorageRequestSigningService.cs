using System;
using System.Linq;
using System.Net;
using System.Text;
using Ninefold.API.Core;

namespace Ninefold.API.Storage
{
    public class StorageRequestSigningService : IRequestSigningService
    {
        public string GenerateRequestSignature(WebRequest request, string base64Secret)
        {
            var secret = Convert.FromBase64String(base64Secret);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);

            var canonHeaders = request.Headers.AllKeys
                .SkipWhile(h => h.Equals("x-emc-date", StringComparison.InvariantCultureIgnoreCase))
                .Where(h => h.StartsWith("x-emc"))                
                .OrderBy(h => h.ToLowerInvariant())
                .Select(h => h + ":" + request.Headers[h].Trim())
                .Aggregate(string.Empty, (current, header) => current + header + "\n")
                .TrimEnd('\n');

            var contentType = request.ContentType ?? string.Empty;
            var range = request.Headers["range"] ?? string.Empty;
            var date = request.Headers["x-emc-date"] ?? request.Headers["date"];
            var requestRelativeUri = request.RequestUri.Segments.Aggregate(string.Empty, (current, segment) => current + segment);

            var hashString = request.Method + "\n" +
                             contentType + "\n" +
                             range + "\n" +
                             date + "\n" +
                              requestRelativeUri + "\n" +
                             canonHeaders;

            return Convert.ToBase64String(hashingAlg.ComputeHash(Encoding.Default.GetBytes(hashString)));
        }
    }
}
