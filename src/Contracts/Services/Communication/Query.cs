using Contracts.Abstractions.Messages;
using Contracts.Services.Communication.Protobuf;

namespace Contracts.Services.Communication;

public static class Query
{
    public record ListEmails(ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListEmailsRequest(ListEmails query)
            => new()
            {
                Limit = query.Limit,
                Offset = query.Offset
            };
    }
}