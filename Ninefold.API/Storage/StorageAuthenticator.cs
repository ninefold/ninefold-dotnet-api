using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public class StorageAuthenticator : IAuthenticator
    {
        readonly string _secret;

        public StorageAuthenticator(string secret)
        {
            _secret = secret;
        }

        public void Authenticate(RestClient client, RestRequest request)
        {
            var headers = request.Parameters.Where(p => p.Type == ParameterType.HttpHeader)
                .OrderBy(p => p.Name);

            var uri = client.BuildUri(request);
            var baseHeaders = client.HttpFactory.Create().Headers;
            var signHashString = request.Method + "\n"
                                 + baseHeaders.FirstOrDefault(h => h.Name == "content-type").Value + "\n"
                                 + baseHeaders.FirstOrDefault(h => h.Name == "range").Value + "\n"
                                 + headers.Where(h => h.Name == "x-emc-date") + "\n"
                                 + client.BuildUri(request) + "\n"
                                 +
                                 headers.SelectMany(
                                     h =>
                                     h.Name.ToLowerInvariant() + ":" +
                                     h.Value.ToString().ToLowerInvariant().Replace("  ", " "));

            var secret = Convert.FromBase64String(_secret);
            var url = Encoding.Default.GetBytes(uri.ToString());
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(secret);
            
            var signature = Convert.ToBase64String(hashingAlg.ComputeHash(url));

            request.AddHeader("x-emc-signature", signature);

            //HTTPRequestMethod + '\n' +  
            //ContentType + '\n' +  
            //Range + '\n' +  
            //Date + '\n' +  
            //CanonicalizedResource + '\n' +  
            //CanonicalizedNINEFOLDHeaders   
        }
    }
}
