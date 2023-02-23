using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Communication;
using Contracts.Services.Communication.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class CommunicationGrpcService : CommunicationService.CommunicationServiceBase
{
    private readonly IPagedInteractor<Query.ListNotificationsDetails, Projection.NotificationDetails> _listNotificationsDetails;

    public CommunicationGrpcService(IPagedInteractor<Query.ListNotificationsDetails, Projection.NotificationDetails> listNotificationsDetails)
    {
        _listNotificationsDetails = listNotificationsDetails;
    }

    public override async Task<ListResponse> ListNotificationsDetails(ListNotificationsDetailsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listNotificationsDetails.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((NotificationDetails)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}