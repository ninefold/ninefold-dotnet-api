using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.API.Storage
{
    public interface IStorageClient
    {
        IStoredObject StoredObject { get; }
    }
}
