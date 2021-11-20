using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPalPaymentMethod : PaymentMethod
{
    protected override bool Validate() => throw new NotImplementedException();
}