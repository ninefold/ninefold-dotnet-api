using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
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
                var exception = new NinefoldApiException(ex);

                if (ex.Response.ContentLength > 0)
                {
                    var responseStream = ex.Response.GetResponseStream();
                    if ((responseStream != null) && (responseStream.CanRead))
                    {
                        var contentDocument = XDocument.Load(responseStream);
                        var message = contentDocument.Root.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("message", StringComparison.InvariantCultureIgnoreCase));
                        var code = contentDocument.Root.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("code", StringComparison.InvariantCultureIgnoreCase));

                        exception.ErrorMessage = message == null ? string.Empty : message.Value;
                        exception.Code = code == null ? string.Empty : code.Value;
                    }        
                }

                throw exception;
            }
        }
    }
}
