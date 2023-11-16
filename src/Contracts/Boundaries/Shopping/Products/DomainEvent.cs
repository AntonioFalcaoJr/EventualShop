using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.Products;

public static class DomainEvent
{
    public record ProductCreated(string ProductId, string Name, decimal Price, string Currency, string Version) : Message, IDomainEvent;
}