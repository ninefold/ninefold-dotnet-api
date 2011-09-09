using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListAccountsResponse : ICommandResponse
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}