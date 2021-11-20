using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Http;

namespace Application.Services.CreditCards.Http;

public interface ICreditCardHttpClient
{
    Task<HttpResponse<CreditCardPaymentResult>> AuthorizeAsync(Requests.ThirdPartyCreditCardAuthorizePayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> RefundAsync(Guid transactionId, Requests.ThirdPartyCreditCardRefundPayment request, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> CaptureAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<HttpResponse<CreditCardPaymentResult>> CancelAsync(Guid transactionId, Requests.ThirdPartyCreditCardCancelPayment request, CancellationToken cancellationToken);
}