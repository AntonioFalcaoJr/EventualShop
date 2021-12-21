using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Http;
using Application.Services.CreditCards;
using Application.Services.CreditCards.Http;
using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.CreditCards;

public class CreditCardHttpClient : ApplicationHttpClient, ICreditCardHttpClient
{
    private readonly CreditCardHttpClientOptions _options;

    public CreditCardHttpClient(IOptionsMonitor<CreditCardHttpClientOptions> optionsMonitor, HttpClient client)
        : base(client)
    {
        _options = optionsMonitor.CurrentValue;
    }

    public Task<HttpResponse<CreditCardPaymentResult>> AuthorizeAsync(Requests.CreditCardAuthorizePayment request, CancellationToken cancellationToken)
        => PostAsync<Requests.CreditCardAuthorizePayment, CreditCardPaymentResult>(_options.AuthorizeEndpoint, request, cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken)
        => GetAsync<CreditCardPaymentResult>($"{_options.CaptureEndpoint}/{transactionId}", cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> CancelAsync(Guid transactionId, Requests.CreditCardCancelPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.CreditCardCancelPayment, CreditCardPaymentResult>($"{_options.CancelEndpoint}/{transactionId}", request, cancellationToken);

    public Task<HttpResponse<CreditCardPaymentResult>> RefundAsync(Guid transactionId, Requests.CreditCardRefundPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.CreditCardRefundPayment, CreditCardPaymentResult>($"{_options.RefundEndpoint}/{transactionId}", request, cancellationToken);
}