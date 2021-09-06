using System;

namespace Messages.Customers.Queries.Responses
{
    public interface CustomerDetails
    {
        Guid Id { get; }
        string Name { get; }
        int Age { get; }
    }
}