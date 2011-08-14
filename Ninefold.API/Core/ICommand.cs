using System.Net;

namespace Ninefold.API.Core
{
    public interface ICommand
    {
        HttpWebRequest Prepare();
        ICommandResponse ParseResponse(WebResponse webResponse);
    }
}
