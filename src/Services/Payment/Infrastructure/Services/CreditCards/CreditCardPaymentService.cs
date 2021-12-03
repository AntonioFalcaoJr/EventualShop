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

    public override Task<IPaymentResult> HandleAsync(IPaymentMethod method, CancellationToken cancellationToken)
        => method is CreditCardPaymentMethod 
            ? AuthorizeAsync(method, cancellationToken)
            : base.HandleAsync(method, cancellationToken);


    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as CreditCardPaymentMethod;

        var request = new Requests.CreditCardAuthorizePayment
        {
            // Use method to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).ActionResult;
    }
}