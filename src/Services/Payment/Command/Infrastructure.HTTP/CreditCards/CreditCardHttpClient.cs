using Application.Abstractions.Http;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Infrastructure.HTTP.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.HTTP.CreditCards;

public class CreditCardHttpClient(IOptionsSnapshot<CreditCardHttpClientOptions> options, HttpClient client)
    : ApplicationHttpClient(client), ICreditCardHttpClient
{
    private readonly CreditCardHttpClientOptions _options = options.Value;

    public Task<HttpResponse<CreditCardPaymentResult>> AuthorizeAsync(Requests.CreditCardAuthorizePayment request, CancellationToken cancellationToken)
        => PostAsync<Requests.CreditCardAuthorizePayment, CreditCardPaymentResult>(_options.AuthorizeEndpoint, request, cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken)
        => GetAsync<CreditCardPaymentResult>($"{_options.CaptureEndpoint}/{transactionId}", cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> CancelAsync(Guid transactionId, Requests.CreditCardCancelPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.CreditCardCancelPayment, CreditCardPaymentResult>($"{_options.CancelEndpoint}/{transactionId}", request, cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> RefundAsync(Guid transactionId, Requests.CreditCardRefundPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.CreditCardRefundPayment, CreditCardPaymentResult>($"{_options.RefundEndpoint}/{transactionId}", request, cancellationToken);
}