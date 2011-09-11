using Ninefold.Compute.Commands;
using Ninefold.Compute.Messages;

namespace Ninefold.Compute
{
    public interface IComputeClient
    {
        ListTemplatesResponse ListTemplates(ListTemplatesRequest request);
        ListAccountsResponse ListAccounts(ListAccountsRequest request);
        ListServiceOfferingsResponse ListServiceOfferings(ListServiceOfferingsRequest request);
    }
}
