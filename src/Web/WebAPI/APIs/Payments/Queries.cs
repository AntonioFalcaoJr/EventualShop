using Contracts.Services.Payment.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Payments.Validators;

namespace WebAPI.APIs.Payments;

public static class Queries
{
    public record GetPaymentDetails(PaymentService.PaymentServiceClient Client, Guid PaymentId, CancellationToken CancellationToken)
        : Validatable<GetPaymentDetailsValidator>, IQuery<PaymentService.PaymentServiceClient>
    {
        public static implicit operator GetPaymentDetailsRequest(GetPaymentDetails request)
            => new() { PaymentId = request.PaymentId.ToString() };
    }

    public record GetPaymentMethodDetails(PaymentService.PaymentServiceClient Client, Guid PaymentId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<GetPaymentMethodDetailsValidator>, IQuery<PaymentService.PaymentServiceClient>
    {
        public static implicit operator GetPaymentMethodDetailsRequest(GetPaymentMethodDetails request)
            => new() { PaymentId = request.PaymentId.ToString(), MethodId = request.MethodId.ToString() };
    }

    public record ListPaymentMethodListItem(PaymentService.PaymentServiceClient Client, Guid PaymentId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListPaymentMethodListItemValidator>, IQuery<PaymentService.PaymentServiceClient>
    {
        public static implicit operator ListPaymentMethodListItemRequest(ListPaymentMethodListItem request)
            => new() { PaymentId = request.PaymentId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}