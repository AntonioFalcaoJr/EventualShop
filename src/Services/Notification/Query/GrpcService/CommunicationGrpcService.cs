using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Boundaries.Notification;
using Contracts.Services.Communication.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class CommunicationGrpcService(IPagedInteractor<Query.ListNotificationsDetails, Projection.NotificationDetails> listNotificationsDetails)
    : CommunicationService.CommunicationServiceBase
{
    public override async Task<ListResponse> ListNotificationsDetails(ListNotificationsDetailsRequest request, ServerCallContext context)
    {
        var pagedResult = await listNotificationsDetails.InteractAsync(request, context.CancellationToken);

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