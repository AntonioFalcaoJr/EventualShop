using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.DebitCards;
using Application.Services.PayPal;
using Contracts.Boundaries.Payment;
using Domain.Aggregates;

namespace Application.Services;

public class PaymentGateway : IPaymentGateway
{
    private readonly IPaymentService _paymentService;

    public PaymentGateway(
        ICreditCardPaymentService creditCardPaymentService,
        IDebitCardPaymentService debitCardPaymentService,
        IPayPalPaymentService payPalPaymentService)
    {
        creditCardPaymentService
            .SetNext(debitCardPaymentService)
            .SetNext(payPalPaymentService);

        _paymentService = creditCardPaymentService;
    }

    public async Task AuthorizeAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.AuthorizeAsync(mtd, ct), method, cancellationToken);

            payment.Handle(paymentResult.Success
                ? new Command.AuthorizePaymentMethod(payment.Id, method.Id, paymentResult.TransactionId)
                : new Command.DenyPaymentMethod(payment.Id, method.Id, paymentResult.TransactionId) as dynamic);
        }
    }

    public async Task CancelAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.CancelAsync(mtd, ct), method, cancellationToken);

            payment.Handle(paymentResult.Success
                ? new Command.CancelPaymentMethod(payment.Id, method.Id, paymentResult.TransactionId)
                : new Command.DenyPaymentMethodCancellation(payment.Id, method.Id, paymentResult.TransactionId) as dynamic);
        }
    }

    public async Task RefundAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync((srv, mtd, ct) => srv.RefundAsync(mtd, ct), method, cancellationToken);

            payment.Handle(paymentResult.Success
                ? new Command.RefundPaymentMethod(payment.Id, method.Id, paymentResult.TransactionId)
                : new Command.DenyPaymentMethodRefund(payment.Id, method.Id, paymentResult.TransactionId) as dynamic);
        }
    }
}