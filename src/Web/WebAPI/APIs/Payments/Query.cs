using Contracts.Services.Payment.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Payments.Validators;

namespace WebAPI.APIs.Payments;

public static class Query
{
    public record GetPayment(PaymentService.PaymentServiceClient Client, Guid PaymentId, CancellationToken CancellationToken)
        : Validatable<GetPaymentValidator>, IQuery<PaymentService.PaymentServiceClient>
    {
        public static implicit operator GetPaymentRequest(GetPayment request)
            => new() { PaymentId = request.PaymentId.ToString() };
  
    }
}