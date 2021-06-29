using Domain.Abstractions.Events;

namespace Domain.Entities.Customers.Events
{
    public record CustomerCreated(string FieldString, int FieldNumber) : IEvent;
}