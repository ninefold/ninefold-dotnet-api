using System;

namespace Ninefold.Core
{
    public interface ICommandRequest
    {
        Uri Resource { get; set; }
    }
}