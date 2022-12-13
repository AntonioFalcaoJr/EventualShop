namespace Application.Services.PushesWeb.Http;

public interface IPayPalHttpClient
{
    Task<HttpResponse<PaypalNotificationResult>> AuthorizeAsync(Requests.PaypalAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalNotificationResult>> RefundAsync(Guid transactionId, Requests.PaypalRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalNotificationResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalNotificationResult>> CancelAsync(Guid transactionId, Requests.PaypalCancelPayment request, CancellationToken cancellationToken);
}