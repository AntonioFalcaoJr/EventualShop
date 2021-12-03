using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using Domain.Entities.PaymentMethods;
using Domain.Entities.PaymentMethods.PayPal;

namespace Infrastructure.Services.PayPal;

public class PayPalPaymentService : PaymentService, IPayPalPaymentService
{
    private readonly IPayPalHttpClient _client;

    public PayPalPaymentService(IPayPalHttpClient client)
    {
        _client = client;
    }

    public override Task<IPaymentResult> HandleAsync(IPaymentMethod method, CancellationToken cancellationToken)
        => method is PayPalPaymentMethod
            ? AuthorizeAsync(method, cancellationToken)
            : base.HandleAsync(method, cancellationToken);

    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        method = method as PayPalPaymentMethod;

        var request = new Requests.PaypalAuthorizePayment
        {
            // Use method to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).ActionResult;
    }
}