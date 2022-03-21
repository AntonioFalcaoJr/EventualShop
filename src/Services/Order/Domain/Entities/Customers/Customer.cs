using Domain.Abstractions.Entities;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Customers;

public class Customer : Entity<Guid>
{
    public Customer(Guid id, Address billingAddress, Address shippingAddress)
    {
        Id = id;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
    }

    public Address ShippingAddress { get; }
    public Address BillingAddress { get; }

    protected override bool Validate()
        => OnValidate<CustomerValidator, Customer>();
}