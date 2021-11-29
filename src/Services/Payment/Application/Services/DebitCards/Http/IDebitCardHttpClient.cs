using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Http;

namespace Application.Services.DebitCards.Http;

public interface IDebitCardHttpClient
{
    Task<HttpResponse<DebitCardPaymentResult>> AuthorizeAsync(Requests.DebitCardAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardPaymentResult>> RefundAsync(Guid transactionId, Requests.DebitCardRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<DebitCardPaymentResult>> CancelAsync(Guid transactionId, Requests.DebitCardCancelPayment request, CancellationToken cancellationToken);
}