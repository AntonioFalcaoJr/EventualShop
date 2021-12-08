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

    public async Task ProceedWithPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var method in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync(method, cancellationToken);

            payment.Handle(new Commands.UpdatePaymentMethod(
                payment.Id,
                method.Id,
                paymentResult.TransactionId,
                paymentResult.Success));
        }
    }
}