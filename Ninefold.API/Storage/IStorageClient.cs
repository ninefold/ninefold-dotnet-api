using Ninefold.Core;

namespace Ninefold.Storage
{
    public interface IStorageClient
    {
        IStorageCommandBuilder Builder { get; }
        IStorageCommandAuthenticator Authenticator { get; }
    }
}
