using System;

namespace Ninefold.API.Core
{
    public interface ICommandRequest
    {
        Uri Resource { get; set; }
    }
}