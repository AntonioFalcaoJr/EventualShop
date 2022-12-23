using Contracts.Abstractions.Messages;

namespace Contracts.Services.Order;

public static class Query
{
    public record GetOrder(Guid OrderId) : IQuery
    {
        // public static implicit operator GetOrder(GetOrderRequest request)
        //     => new(new Guid(request.Id));
    }

    public record ListOrders(ushort Limit, ushort Offset) : IQuery
    {
        // public static implicit operator ListAccounts(ListAccountsOrders request)
        //     => new((ushort)request.Limit, (ushort)request.Offset);
    }
}