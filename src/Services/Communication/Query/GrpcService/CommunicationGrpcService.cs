using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication;
using Contracts.Services.Communication.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class CommunicationGrpcService : CommunicationService.CommunicationServiceBase
{
    private readonly IInteractor<Query.ListNotifications, IPagedResult<Projection.NotificationDetails>> _listEmailsInteractor;

    public CommunicationGrpcService(IInteractor<Query.ListNotifications, IPagedResult<Projection.NotificationDetails>> listEmailsInteractor)
    {
        _listEmailsInteractor = listEmailsInteractor;
    }

    public override async Task<Notifications> ListEmails(ListNotificationsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listEmailsInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(details => (Notification)details) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }
}