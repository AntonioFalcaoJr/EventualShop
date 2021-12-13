using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.DebitCards;
using Application.Services.PayPal;
using Domain.Aggregates;
using ECommerce.Contracts.Payment;

namespace Application.Services;

public class PaymentStrategy : IPaymentStrategy
{
    private readonly IPaymentService _paymentService;

    public PaymentStrategy(
        ICreditCardPaymentService creditCardPaymentService,
        IDebitCardPaymentService debitCardPaymentService,
        IPayPalPaymentService payPalPaymentService)
    {
        creditCardPaymentService
            .SetNext(debitCardPaymentService)
            .SetNext(payPalPaymentService);

        _paymentService = creditCardPaymentService;
    }

    public async Task AuthorizePaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.AuthorizeAsync(mtd, ct), method, cancellationToken);

            payment.Handle(new Commands.UpdatePaymentMethod(
                payment.Id,
                method.Id,
                paymentResult.TransactionId,
                /* TODO - paymentResult.Success */ true));
        }
    }

    public async Task CancelPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.CancelAsync(mtd, ct), method, cancellationToken);

            payment.Handle(new Commands.UpdatePaymentMethod(
                payment.Id,
                method.Id,
                paymentResult.TransactionId,
                /* TODO - paymentResult.Success */ true));
        }
    }

    public async Task RefundPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.RefundAsync(mtd, ct), method, cancellationToken);

            payment.Handle(new Commands.UpdatePaymentMethod(
                payment.Id,
                method.Id,
                paymentResult.TransactionId,
                /* TODO - paymentResult.Success */ true));
        }
    }
}