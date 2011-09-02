using System.Net;

namespace Ninefold.Core
{
    public interface ICommand
    {
        HttpWebRequest Prepare();
        ICommandResponse ParseResponse(WebResponse webResponse);
    }
}
