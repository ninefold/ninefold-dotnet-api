using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;
using Ninefold.API.Storage;

namespace Ninefold.API
{
    public class StorageClient : ICommandExecutor
    {
        const string DefaultStorageRootUrl = "http://onlinestorage.ninefold.com";

        public StoredObject StoredObject { get; set; }

        public StorageClient(string apiKey, string base64Secret)
            : this (apiKey, base64Secret, DefaultStorageRootUrl)
        { }

        public StorageClient(string apiKey, string base64Secret, string storageServiceRootUrl)
        {
            StoredObject = new StoredObject(apiKey, base64Secret, storageServiceRootUrl);
        }

        public ICommandResponse Execute(ICommand command)
        {
            return command.Execute();
        }
    }
}
