using Application.Abstractions.Http;

namespace Application.Services.CreditCards.Http;

public interface ICreditCardHttpClient
{
    Task<HttpResponse<CreditCardPaymentResult>> AuthorizeAsync(Requests.CreditCardAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> RefundAsync(Guid transactionId, Requests.CreditCardRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> CancelAsync(Guid transactionId, Requests.CreditCardCancelPayment request, CancellationToken cancellationToken);
}