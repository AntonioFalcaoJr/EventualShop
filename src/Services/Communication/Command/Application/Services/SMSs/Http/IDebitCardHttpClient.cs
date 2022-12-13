namespace Application.Services.SMSs.Http;

public interface IDebitCardHttpClient
{
    Task<HttpResponse<DebitCardNotificationResult>> AuthorizeAsync(Requests.DebitCardAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardNotificationResult>> RefundAsync(Guid transactionId, Requests.DebitCardRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardNotificationResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardNotificationResult>> CancelAsync(Guid transactionId, Requests.DebitCardCancelPayment request, CancellationToken cancellationToken);
}