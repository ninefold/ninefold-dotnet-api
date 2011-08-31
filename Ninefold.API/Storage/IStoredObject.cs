using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public interface IStoredObject
    {
        CreateObjectResponse CreateObject(CreateObjectRequest request);
        DeleteObjectResponse DeleteObject(DeleteObjectRequest request);
        GetObjectResponse GetObject(GetObjectRequest request);
        UpdateObjectResponse UpdateObject(UpdateObjectRequest request);
        ListNamespaceResponse ListNamespace(ListNamespaceRequest request);
        SetObjectACLResponse SetObjectACL(SetObjectACLRequest request);
        DeleteUserMetadataResponse DeleteUserMetadata(DeleteUserMetadataRequest request);
        GetObjectAclResponse GetObjectACL(GetObjectAclRequest request);
        GetListableTagsResponse GetListableTags(GetListableTagsRequest request);
        GetSystemMetadataResponse GetSystemMetadata(GetSystemMetadataRequest request);
        GetUserMetadataResponse GetUserMetadata(GetUserMetadataRequest request);
        ListObjectsResponse ListObjects(ListObjectsRequest request);
    }
}
