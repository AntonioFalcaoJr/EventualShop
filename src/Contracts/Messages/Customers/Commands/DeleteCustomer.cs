using System;

namespace Messages.Customers.Commands
{
    public interface DeleteCustomer
    {
        Guid Id { get; }
    }
}