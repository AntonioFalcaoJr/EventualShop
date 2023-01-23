using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Services.Payment;

public static class Query
{
    public record GetPaymentDetails(Guid PaymentId) : IQuery
    {
        public static implicit operator GetPaymentDetails(Protobuf.GetPaymentDetailsRequest request)
            => new(new(request.PaymentId));
    }

    public record GetPaymentMethodDetails(Guid MethodId) : IQuery
    {
        public static implicit operator GetPaymentMethodDetails(Protobuf.GetPaymentMethodDetailsRequest request)
            => new(new(request.MethodId));
    }

    public record ListPaymentMethodListItem(Guid PaymentId, Paging Paging) : IQuery
    {
        public static implicit operator ListPaymentMethodListItem(Protobuf.ListPaymentMethodListItemRequest request)
            => new(new(request.PaymentId), request.Paging);
    }
}