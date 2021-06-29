using Domain.Abstractions.Events;

namespace Domain.Entities.Customers.Events
{
    public record CustomerNameChanged(string Name) : IEvent;
}