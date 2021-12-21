using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Http;

namespace Application.Services.PayPal.Http;

public interface IPayPalHttpClient
{
    Task<HttpResponse<PaypalPaymentResult>> AuthorizeAsync(Requests.PaypalAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalPaymentResult>> RefundAsync(Guid transactionId, Requests.PaypalRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<PaypalPaymentResult>> CancelAsync(Guid transactionId, Requests.PaypalCancelPayment request, CancellationToken cancellationToken);
}