using Application.Abstractions.Http;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using Infrastructure.HTTP.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.HTTP.PayPals;

public class PayPalHttpClient : ApplicationHttpClient, IPayPalHttpClient
{
    private readonly PayPalHttpClientOptions _options;

    public PayPalHttpClient(IOptionsSnapshot<PayPalHttpClientOptions> options, HttpClient client) : base(client) 
        => _options = options.Value;

    public Task<HttpResponse<PaypalPaymentResult>> AuthorizeAsync(Requests.PaypalAuthorizePayment request, CancellationToken cancellationToken)
        => PostAsync<Requests.PaypalAuthorizePayment, PaypalPaymentResult>(_options.AuthorizeEndpoint, request, cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken)
        => GetAsync<PaypalPaymentResult>($"{_options.CaptureEndpoint}/{transactionId}", cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> CancelAsync(Guid transactionId, Requests.PaypalCancelPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.PaypalCancelPayment, PaypalPaymentResult>($"{_options.CancelEndpoint}/{transactionId}", request, cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> RefundAsync(Guid transactionId, Requests.PaypalRefundPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.PaypalRefundPayment, PaypalPaymentResult>($"{_options.RefundEndpoint}/{transactionId}", request, cancellationToken);
}