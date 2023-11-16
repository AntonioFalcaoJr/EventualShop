using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.PaymentOptions.CreditCards;

namespace Infrastructure.HTTP.CreditCards;

public class CreditCardPaymentService(ICreditCardHttpClient client) : PaymentService, ICreditCardPaymentService
{
    public override Task<IPaymentResult>? HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken)
        => method.Option is CreditCard
            ? behaviorProcessor(this, method, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult?> AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate  
        Requests.CreditCardAuthorizePayment request = new();
        var response = await client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate  
        Requests.CreditCardCancelPayment request = new();
        var response = await client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate
        Requests.CreditCardRefundPayment request = new();
        var response = await client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}