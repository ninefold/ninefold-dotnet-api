using System;

namespace Ninefold.Storage
{
    public interface IStorageCommandRequest
    {
        Uri Resource { get; set; }
    }
}