using System;
using Domain.Abstractions.DomainEvents;

namespace Domain.Entities.Customers
{
    public abstract record CustomerDomainEvent : DomainEvent;

    public record CustomerAgeChangedDomainEvent(int Age) : CustomerDomainEvent;

    public record CustomerNameChangedDomainEvent(string Name) : CustomerDomainEvent;

    public record CustomerRegisteredDomainEvent(Guid Id, string Name, int Age) : CustomerDomainEvent;
}