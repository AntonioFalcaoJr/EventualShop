using System;

namespace Messages.Customers.Commands
{
    public interface UpdateCustomer
    {
        Guid Id { get; }
        string Name { get; }
        int Age { get; }
    }
}