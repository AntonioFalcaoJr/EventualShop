using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.CreditCards;

namespace Infrastructure.Services.CreditCards;

public class CreditCardPaymentService : PaymentService, ICreditCardPaymentService
{
    private readonly ICreditCardHttpClient _client;

    public CreditCardPaymentService(ICreditCardHttpClient client)
    {
        _client = client;
    }

    public override async Task<IPaymentResult> HandleAsync(IPaymentMethod paymentMethod, CancellationToken cancellationToken)
        => paymentMethod is CreditCardPaymentMethod
            ? await AuthorizeAsync(paymentMethod, cancellationToken)
            : await Next.HandleAsync(paymentMethod, cancellationToken);


    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.ThirdPartyCreditCardAuthorizePayment
        {
            // Use IPaymentMethod to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).PayloadResult;
    }
}