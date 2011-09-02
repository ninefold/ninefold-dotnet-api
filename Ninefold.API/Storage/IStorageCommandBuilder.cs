using System.Net;
using Ninefold.Core;

namespace Ninefold.Storage
{
    public interface IStorageCommandBuilder
    {
        HttpWebRequest GenerateRequest(ICommandRequest request, string userId, string requestMethod);
    }
}