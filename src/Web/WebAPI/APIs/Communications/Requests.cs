using Contracts.Services.Communication.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Communications.Validators;

namespace WebAPI.APIs.Communications;

public static class Requests
{
    public record ListNotificationsDetails(CommunicationService.CommunicationServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListNotificationsDetailsValidator>, IQueryRequest<CommunicationService.CommunicationServiceClient>
    {
        public static implicit operator ListNotificationsDetailsRequest(ListNotificationsDetails request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}