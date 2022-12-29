using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Query
{
    public record struct ListNotifications(Paging Paging) : IQuery
    {
        public static implicit operator ListNotifications(ListNotificationsRequest request)
            => new(request.Paging);
    }
}