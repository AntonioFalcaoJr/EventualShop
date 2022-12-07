using Contracts.Abstractions.Messages;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Query
{
    public record ListNotifications(ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListNotifications(ListNotificationsRequest request)
            => new((ushort)request.Limit, (ushort)request.Offset);
    }
}