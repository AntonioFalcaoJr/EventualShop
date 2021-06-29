using Domain.Abstractions.Events;

namespace Domain.Entities.Customers.Events
{
    public record CustomerAgeChanged(int Age) : IEvent;
}