using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.PaymentOptions.CreditCards;

namespace Infrastructure.HttpClients.CreditCards;

public class CreditCardPaymentService : PaymentService, ICreditCardPaymentService
{
    private readonly ICreditCardHttpClient _client;

    public CreditCardPaymentService(ICreditCardHttpClient client)
    {
        _client = client;
    }

    public override Task<IPaymentResult> HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken)
        => method.Option is CreditCard
            ? behaviorProcessor(this, method, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult> AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.CreditCardAuthorizePayment request = new()
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.CreditCardCancelPayment request = new()
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        Requests.CreditCardRefundPayment request = new()
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}