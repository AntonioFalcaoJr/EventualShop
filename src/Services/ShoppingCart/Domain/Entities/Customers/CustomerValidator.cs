using Domain.Abstractions.Validators;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Customers;

public class CustomerValidator : EntityValidator<Customer, Guid>
{
    public CustomerValidator()
    {
        RuleFor(cart => cart.BillingAddress)
            .SetValidator(new AddressValidator());

        RuleFor(cart => cart.ShippingAddress)
            .SetValidator(new AddressValidator());
    }
}