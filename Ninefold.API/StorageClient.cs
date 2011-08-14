using System.Net;
using Ninefold.API.Core;
using Ninefold.API.Storage;

namespace Ninefold.API
{
    public class StorageClient : ICommandExecutor
    {
        const string DefaultStorageRootUrl = "http://onlinestorage.ninefold.com/rest/";

        public IStoredObject StoredObject { get; private set; }

        public StorageClient(string userId, string base64Secret)
            : this (userId, base64Secret, DefaultStorageRootUrl)
        { }

        public StorageClient(string userId, string base64Secret, string storageServiceRootUrl)
        {
            StoredObject = new StoredObject(userId, base64Secret, storageServiceRootUrl);
        }

        ICommandResponse ICommandExecutor.Execute(ICommand command)
        {
            var request = command.Prepare();

            try
            {
                var webResponse = (HttpWebResponse)request.GetResponse();
                return command.ParseResponse(webResponse);
            }
            catch (WebException ex)
            {
                throw new NinefoldApiException(ex) { NinefoldErrorMessage = ex.Message };
            }
        }
    }
}
