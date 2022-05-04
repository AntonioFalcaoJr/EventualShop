using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;

namespace Infrastructure.HttpClients.CreditCards;

public class CreditCardPaymentService : PaymentService, ICreditCardPaymentService
{
    private readonly ICreditCardHttpClient _client;

    public CreditCardPaymentService(ICreditCardHttpClient client)
    {
        _client = client;
    }

    public override Task<IPaymentResult> HandleAsync(Func<IPaymentService, IPaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, IPaymentMethod method, CancellationToken cancellationToken)
        => method is CreditCard creditCardPaymentMethod
            ? behaviorProcessor(this, creditCardPaymentMethod, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.CreditCardAuthorizePayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.CreditCardCancelPayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.CreditCardRefundPayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}