﻿using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class ListInventoryGridItemsValidator : AbstractValidator<Queries.ListInventoryGridItems>
{
    public ListInventoryGridItemsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}

// ListInventoryItemsListItems