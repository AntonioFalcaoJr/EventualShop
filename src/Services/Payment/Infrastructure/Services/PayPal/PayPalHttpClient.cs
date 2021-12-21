using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Http;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.PayPal;

public class PayPalHttpClient : ApplicationHttpClient, IPayPalHttpClient
{
    private readonly PayPalHttpClientOptions _options;

    public PayPalHttpClient(IOptionsMonitor<PayPalHttpClientOptions> optionsMonitor, HttpClient client)
        : base(client)
    {
        _options = optionsMonitor.CurrentValue;
    }

    public Task<HttpResponse<PaypalPaymentResult>> AuthorizeAsync(Requests.PaypalAuthorizePayment request, CancellationToken cancellationToken)
        => PostAsync<Requests.PaypalAuthorizePayment, PaypalPaymentResult>(_options.AuthorizeEndpoint, request, cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken)
        => GetAsync<PaypalPaymentResult>($"{_options.CaptureEndpoint}/{transactionId}", cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> CancelAsync(Guid transactionId, Requests.PaypalCancelPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.PaypalCancelPayment, PaypalPaymentResult>($"{_options.CancelEndpoint}/{transactionId}", request, cancellationToken);

    public Task<HttpResponse<PaypalPaymentResult>> RefundAsync(Guid transactionId, Requests.PaypalRefundPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.PaypalRefundPayment, PaypalPaymentResult>($"{_options.RefundEndpoint}/{transactionId}", request, cancellationToken);
}