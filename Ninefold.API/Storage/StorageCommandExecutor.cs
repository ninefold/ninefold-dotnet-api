using System.Net;
using Ninefold.API.Core;

namespace Ninefold.API.Storage
{
    public class StorageCommandExecutor : ICommandExecutor
    {
        public ICommandResponse Execute(ICommand command)
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
