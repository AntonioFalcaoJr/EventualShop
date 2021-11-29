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

    public override async Task<IPaymentResult> HandleAsync(IPaymentMethod paymentMethod, CancellationToken cancellationToken)
        => paymentMethod is DebitCardPaymentMethod debitCardPaymentMethod
            ? await AuthorizeAsync(debitCardPaymentMethod, cancellationToken)
            : await Next.HandleAsync(paymentMethod, cancellationToken);


    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.DebitCardAuthorizePayment()
        {
            // Use IPaymentMethod to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).ActionResult;
    }
}