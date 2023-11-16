using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Boundaries.Payment;

public static class Query
{
    public record GetPaymentDetails(Guid PaymentId) : IQuery
    {
        public static implicit operator GetPaymentDetails(Services.Payment.Protobuf.GetPaymentDetailsRequest request)
            => new(new Guid(request.PaymentId));
    }

    public record GetPaymentMethodDetails(Guid MethodId) : IQuery
    {
        public static implicit operator GetPaymentMethodDetails(Services.Payment.Protobuf.GetPaymentMethodDetailsRequest request)
            => new(new Guid(request.MethodId));
    }

    public record ListPaymentMethodListItem(Guid PaymentId, Paging Paging) : IQuery
    {
        public static implicit operator ListPaymentMethodListItem(Services.Payment.Protobuf.ListPaymentMethodListItemRequest request)
            => new(new(request.PaymentId), request.Paging);
    }
}