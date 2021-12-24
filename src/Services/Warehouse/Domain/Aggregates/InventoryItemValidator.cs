using System;
using Domain.Abstractions.Validators;

namespace Domain.Aggregates;

public class InventoryItemValidator : EntityValidator<InventoryItem, Guid>
{
    public InventoryItemValidator()
    {
    }
}