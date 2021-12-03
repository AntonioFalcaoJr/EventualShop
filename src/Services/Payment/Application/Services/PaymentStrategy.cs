using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.DebitCards;
using Application.Services.PayPal;
using Domain.Aggregates;
using Messages.Services.Payments;

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

public interface IPaymentStrategy
{
    Task ProceedWithPaymentAsync(Payment payment, CancellationToken cancellationToken);
}

//     public void Handle(Order order)
//     {
//         foreach (var receiver in _receivers)
//         {
//             Console.WriteLine($"Running: {receiver.GetType().Name}");
//
//             if (order.AmountDue > 0)
//             {
//                 receiver.Handle(order);
//             }
//             else
//             {
//                 break;
//             }
//         }
//
//         if (order.AmountDue > 0)
//         {
//             throw new Exception("Insufficient payment");
//         }
//         else
//         {
//             // order.ShippingStatus = ShippingStatus.ReadyForShipment;
//         }
//     }
//