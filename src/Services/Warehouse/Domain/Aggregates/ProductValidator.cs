using System;
using Domain.Abstractions.Validators;

namespace Domain.Aggregates;

public class ProductValidator : EntityValidator<Product, Guid>
{
    public ProductValidator()
    {
    }
}