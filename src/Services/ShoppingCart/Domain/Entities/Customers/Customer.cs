using Domain.Abstractions.Entities;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Customers;

public class Customer : Entity<Guid, CustomerValidator>
{
    public Customer(Guid id)
    {
        Id = id;
    }

    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    private bool ShippingAndBillingAddressesAreSame { get; set; } = true;

    public void SetShippingAddress(Address address)
    {
        ShippingAddress = address;

        if (ShippingAndBillingAddressesAreSame)
            BillingAddress = ShippingAddress;
    }

    public void SetBillingAddress(Address address)
    {
        BillingAddress = address;
        ShippingAndBillingAddressesAreSame = false;
    }
}