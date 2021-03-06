﻿using Ninefold.Compute.Messages;

namespace Ninefold.Compute
{
    public interface IComputeClient
    {
        ListTemplatesResponse ListTemplates(ListTemplatesRequest request);
        ListAccountsResponse ListAccounts(ListAccountsRequest request);
        ListServiceOfferingsResponse ListServiceOfferings(ListServiceOfferingsRequest request);
        ListVirtualMachinesResponse ListVirtualMachines(ListVirtualMachinesRequest request);
        ListTemplatePermissionsResponse ListTemplatePermissions(ListTemplatePermissionsRequest request);
        ListIsosResponse ListIsos(ListIsosRequest request);
        ListIsoPermissionsResponse ListIsoPermissions(ListIsoPermissionsRequest request);
        ListVolumesResponse ListVolumes(ListVolumesRequest request);
        ListSnapshotsResponse ListSnapshots(ListSnapshotsRequest listSnapshotsRequest);
        ListSnapshotPoliciesResponse ListSnapshotPolicies(ListSnapshotPoliciesRequest request);
        ListAsyncJobsResponse ListAsyncJobs(ListAsyncJobsRequest request);
        ListEventsResponse ListEvents(ListEventsRequest request);
    }
}
