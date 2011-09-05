using System.Net;
using Ninefold.Core;

namespace Ninefold.Storage
{
    public interface IStorageCommandBuilder
    {
        HttpWebRequest GenerateRequest(IStorageCommandRequest request, string userId, string requestMethod);
    }
}