using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.Coupons;

public class Coupon : Entity<Guid>
{
    protected override bool Validate() => throw new NotImplementedException();
}