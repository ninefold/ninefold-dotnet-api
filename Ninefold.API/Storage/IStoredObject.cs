using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public interface IStoredObject
    {
        CreateObjectResponse CreateObject(CreateObjectRequest request);
    }
}
