using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Services.CreditCards;
using Application.Services.PayPal;
using Domain.Aggregates;
using Messages.Services.Payments;

namespace Application.Services;

public class PaymentStrategy : IPaymentStrategy
{
    private readonly IPaymentService _paymentService;

    public PaymentStrategy(
        ICreditCardPaymentService creditCardPaymentService,
        IPayPalPaymentService payPalPaymentService)
    {
        
            creditCardPaymentService
                .SetNext(payPalPaymentService); //Invoice Service

            _paymentService = creditCardPaymentService;
    }

    public async Task ProceedWithPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        foreach (var paymentMethod in payment.Methods)
        {
            if (payment.AmountDue <= 0) break;

            var paymentResult = await _paymentService.HandleAsync(paymentMethod, cancellationToken);

            payment.Handle(new Commands.UpdatePaymentMethod(
                payment.Id,
                paymentMethod.Id,
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