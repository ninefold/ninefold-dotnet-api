using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public static CreateObjectResponse Create(string storageServiceBaseUrl,
                                                                    byte[] content,
                                                                    IEnumerable<KeyValuePair<string, string>> acl,
                                                                    IEnumerable<KeyValuePair<string, string>> listableMetadata,
                                                                    IEnumerable<KeyValuePair<string, string>> metadata,
                                                                    IEnumerable<KeyValuePair<string, string>> optionalHeaders = null,
                                                                    string contentType = "application/octet-stream",
                                                                    string groupAcl = "NONE")
        {
            var storageService = new StorageService(storageServiceBaseUrl);
            var createObject = new CreateObject(storageService)
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
