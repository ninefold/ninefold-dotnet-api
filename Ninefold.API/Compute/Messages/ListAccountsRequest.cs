using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.Compute.Messages
{
    public class ListAccountsRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "listAccounts"; }
        }
    }
}
