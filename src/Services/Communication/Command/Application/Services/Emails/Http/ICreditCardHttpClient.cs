namespace Application.Services.Emails.Http;

public interface ICreditCardHttpClient
{
    Task<HttpResponse<CreditCardNotificationResult>> AuthorizeAsync(Requests.CreditCardAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardNotificationResult>> RefundAsync(Guid transactionId, Requests.CreditCardRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardNotificationResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardNotificationResult>> CancelAsync(Guid transactionId, Requests.CreditCardCancelPayment request, CancellationToken cancellationToken);
}