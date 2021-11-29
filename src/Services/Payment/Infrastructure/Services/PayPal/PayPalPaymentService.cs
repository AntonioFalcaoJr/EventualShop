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

    public override async Task<IPaymentResult> HandleAsync(IPaymentMethod paymentMethod, CancellationToken cancellationToken)
        => paymentMethod is PayPalPaymentMethod payPalPaymentMethod
            ? await AuthorizeAsync(payPalPaymentMethod, cancellationToken)
            : await Next.HandleAsync(paymentMethod, cancellationToken);


    protected override async Task<IPaymentResult> AuthorizeAsync(IPaymentMethod method, CancellationToken cancellationToken)
    {
        var request = new Requests.PaypalAuthorizePayment
        {
            // Use IPaymentMethod to hydrate  
        };

        return (await _client.AuthorizeAsync(request, cancellationToken)).ActionResult;
    }
}