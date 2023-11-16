using Application.Abstractions.Services;
using Application.Services.DebitCards;
using Application.Services.DebitCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.PaymentOptions.DebitCards;

namespace Infrastructure.HTTP.DebitCards;

public class DebitCardPaymentService(IDebitCardHttpClient client) : PaymentService, IDebitCardPaymentService
{
    public override Task<IPaymentResult> HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken)
        => method.Option is DebitCard
            ? behaviorProcessor(this, method, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult> AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.DebitCardAuthorizePayment request = new();
        var response = await client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.DebitCardCancelPayment request = new();
        var response = await client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.DebitCardRefundPayment request = new();
        var response = await client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}