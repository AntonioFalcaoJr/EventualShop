using Application.Abstractions.Http;
using Application.Services.DebitCards;
using Application.Services.DebitCards.Http;
using Infrastructure.HttpClients.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.HttpClients.DebitCards;

public class DebitCardHttpClient : ApplicationHttpClient, IDebitCardHttpClient
{
    private readonly DebitCardHttpClientOptions _options;

    public DebitCardHttpClient(IOptionsMonitor<DebitCardHttpClientOptions> optionsMonitor, HttpClient client) : base(client) 
        => _options = optionsMonitor.CurrentValue;

    public Task<HttpResponse<DebitCardPaymentResult>> AuthorizeAsync(Requests.DebitCardAuthorizePayment request, CancellationToken cancellationToken)
        => PostAsync<Requests.DebitCardAuthorizePayment, DebitCardPaymentResult>(_options.AuthorizeEndpoint, request, cancellationToken);

    public Task<HttpResponse<DebitCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken)
        => GetAsync<DebitCardPaymentResult>($"{_options.CaptureEndpoint}/{transactionId}", cancellationToken);

    public Task<HttpResponse<DebitCardPaymentResult>> CancelAsync(Guid transactionId, Requests.DebitCardCancelPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.DebitCardCancelPayment, DebitCardPaymentResult>($"{_options.CancelEndpoint}/{transactionId}", request, cancellationToken);

    public Task<HttpResponse<DebitCardPaymentResult>> RefundAsync(Guid transactionId, Requests.DebitCardRefundPayment request, CancellationToken cancellationToken)
        => PutAsync<Requests.DebitCardRefundPayment, DebitCardPaymentResult>($"{_options.RefundEndpoint}/{transactionId}", request, cancellationToken);
}