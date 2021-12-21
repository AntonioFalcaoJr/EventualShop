using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.DebitCards;
using Application.Services.DebitCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.DebitCards;

namespace Infrastructure.Services.DebitCards;

public class DebitCardPaymentService : PaymentService, IDebitCardPaymentService
{
    private readonly IDebitCardHttpClient _client;

    public DebitCardPaymentService(IDebitCardHttpClient client)
    {
        _client = client;
    }

    public override Task<IPaymentResult> HandleAsync(Func<IPaymentService, IPaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, IPaymentMethod method, CancellationToken cancellationToken)
        => method is DebitCardPaymentMethod
            ? behaviorProcessor(this, method, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as DebitCardPaymentMethod;

        var request = new Requests.DebitCardAuthorizePayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as DebitCardPaymentMethod;

        var request = new Requests.DebitCardCancelPayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as DebitCardPaymentMethod;

        var request = new Requests.DebitCardRefundPayment
        {
            // TODO - Use method to hydrate  
        };

        var response = await _client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}