using Contracts.Services.Communication.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Communications.Validators;

namespace WebAPI.APIs.Communications;

public static class Requests
{
    public record ListNotifications(CommunicationService.CommunicationServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListNotificationsValidator>, IQueryRequest<CommunicationService.CommunicationServiceClient>
    {
        public static implicit operator ListNotificationsRequest(ListNotifications request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}