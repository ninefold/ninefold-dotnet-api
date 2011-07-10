using System.Collections.Generic;
using Ninefold.API.Core;
using Ninefold.API.Storage.Commands;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public class StorageObject
    {
        readonly INinefoldService _storageService;

        public StorageObject(string storageServiceBaseUrl)
        {
            _storageService = new StorageService(storageServiceBaseUrl);
        }


        public CreateObjectResponse Create(byte[] content,
                                                                    IEnumerable<KeyValuePair<string, string>> acl,
                                                                    IEnumerable<KeyValuePair<string, string>> listableMetadata,
                                                                    IEnumerable<KeyValuePair<string, string>> metadata,
                                                                    IEnumerable<KeyValuePair<string, string>> optionalHeaders,
                                                                    string contentType = "application/octet-stream",
                                                                    string groupAcl = "NONE")
        {
            var createObject = new CreateObject(_storageService)
                                   {
                                       ACL = acl,
                                       Content = content,
                                       ContentType = contentType,
                                       GroupACL = groupAcl,
                                       ListableMetadata = listableMetadata,
                                       Metadata = metadata,
                                       OptionalHeaders = optionalHeaders
                                   };

            return createObject.Execute();
        }

    }
}
