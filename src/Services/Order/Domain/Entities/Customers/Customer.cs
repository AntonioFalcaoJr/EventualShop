using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Customers;

public class Customer : Entity<Guid, CustomerValidator>
{
    public Customer(Guid id, Address billingAddress, Address shippingAddress)
    {
        Id = id;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
    }

    public Address ShippingAddress { get; }
    public Address BillingAddress { get; }
    
    public static implicit operator Customer(Dto.Customer customer)
        => new(customer.Id ?? default, customer.BillingAddress, customer.ShippingAddress);

    public static implicit operator Dto.Customer(Customer customer)
        => new(customer.Id, customer.ShippingAddress, customer.BillingAddress);
}