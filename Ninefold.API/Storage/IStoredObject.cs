using Ninefold.API.Storage.Commands;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public interface IStoredObject
    {
        CreateObjectResponse CreateObject(CreateObjectRequest request);
        DeleteObjectResponse DeleteObject(DeleteObjectRequest request);
        GetObjectResponse GetObject(GetObjectRequest request);
    }
}
