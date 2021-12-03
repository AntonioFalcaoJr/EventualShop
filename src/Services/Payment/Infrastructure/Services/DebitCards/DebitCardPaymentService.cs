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

    public override Task<IPaymentResult> HandleAsync(IPaymentMethod method, CancellationToken cancellationToken)
        => method is DebitCardPaymentMethod
            ? AuthorizeAsync(method, cancellationToken)
            : base.HandleAsync(method, cancellationToken);

    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as DebitCardPaymentMethod;

        var request = new Requests.DebitCardAuthorizePayment()
        {
            // Use method to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).ActionResult;
    }
}