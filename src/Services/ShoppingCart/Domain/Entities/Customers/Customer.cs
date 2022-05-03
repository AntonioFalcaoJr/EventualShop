using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Customers;

public class Customer : Entity<Guid, CustomerValidator>
{
    public Customer(Guid id)
    {
        Id = id;
    }

    private Customer(Guid id, Address billingAddress, Address shippingAddress)
    {
        Id = id;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
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

    public static implicit operator Customer(Dto.Customer customer)
        => new(customer.Id ?? default, customer.BillingAddress, customer.ShippingAddress);

    public static implicit operator Dto.Customer(Customer customer)
        => new(customer.Id, customer.ShippingAddress, customer.BillingAddress);
}