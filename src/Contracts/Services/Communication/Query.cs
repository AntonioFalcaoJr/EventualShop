using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Query
{
    public record ListNotificationsDetails(Paging Paging) : IQuery
    {
        public static implicit operator ListNotificationsDetails(ListNotificationsDetailsRequest request)
            => new(request.Paging);
    }
}